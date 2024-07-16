use core::pin::Pin;
use std::time::Instant;



async fn transpose<T>(future: &mut  Option<Pin<Box<impl std::future::Future<Output = T>>>>) -> Option<T> {
    match future {
        Some(future) => {
            Some(future.await)
        },
        None => None
    }
}

#[tokio::main]
async fn main() {
    let mut may_be_future1 = None;
    let mut may_be_future2 = None;
    
    let start = Instant::now();
    loop {
        if Instant::now().duration_since(start).as_secs() % 5 == 0 {
            may_be_future1 = Some(Box::pin(async {
                println!("Future 1");
            }));            
        }

        if Instant::now().duration_since(start).as_secs() % 3 == 0 {
            may_be_future2 = Some(Box::pin(async {
                println!("Future 2");
            }));
        }

        let a = may_be_future1.is_some();
        let b = may_be_future2.is_some();

        if a == b {
            println!("both {a} {b}");
        }

        tokio::select! {
            Some(_) = transpose(&mut may_be_future1) => {
                println!("Future 1 completed");
                may_be_future1 = None;
            },
            Some(_) = transpose(&mut may_be_future2) => {
                println!("Future 2 completed");
                may_be_future2 = None;
            },
            _ = tokio::time::sleep(std::time::Duration::from_secs(1)) => {
                println!("No future completed");
            }
        }
        println!("!!!!!!!!!!!!!! Futures state 1={:?} 2={:?}", may_be_future1.is_some(), may_be_future2.is_some());
        if a && b {
            assert_ne!(may_be_future1.is_some(), may_be_future2.is_some());
        }

        tokio::time::sleep(std::time::Duration::from_secs(1)).await;
    }
    println!("Hello, world!");
}
