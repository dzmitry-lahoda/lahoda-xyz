/// Transpose `&mut Option<impl Future>` into `impl Future<Option>`, similar to `Result::transpose`
pub async fn transpose_future_mut<T>(
    future: &mut Option<impl core::future::Future<Output = T> + Unpin>,
) -> Option<T> {
    match future {
        Some(future) => Some(future.await),
        None => None,
    }
}
use tokio::*;

/// Expect:
/// 2 seconds passed
/// 2 seconds passed
/// 6 seconds passed
/// or
/// 2 seconds passed
/// 2 seconds passed
/// 2 seconds passed
/// 6 seconds passed
#[tokio::main]
async fn main() {
    let mut seconds6 = Some(Box::pin(time::sleep(std::time::Duration::from_secs(6))));
    loop {
        select! {
            Some(_) = transpose_future_mut(&mut seconds6) => {
                eprintln!("6 seconds passed");
                let done = seconds6.take().expect("still here");
                assert!(done.is_elapsed());
                break;
            }
            _ = time::sleep(std::time::Duration::from_secs(2)) => {
                eprintln!("2 seconds passed");
            }
        }
    }
}
