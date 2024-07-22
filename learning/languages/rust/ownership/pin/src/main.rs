use core::pin;
use std::sync::Arc;

fn main() {
    let mut allocated = Arc::new(Box::new("42".to_string()));

    let pinned = pin::Pin::new(&allocated);

    let closure = move || {
        let _ = allocated;
        println!("{allocated}");
        std::thread::spawn(|| {
            println!("{allocated}");
        })
    };

    closure().join().unwrap();
}
