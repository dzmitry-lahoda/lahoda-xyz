#![feature(prelude_import)]
#[prelude_import]
use std::prelude::rust_2024::*;
#[macro_use]
extern crate std;
use auto_impl::auto_impl;
trait X {
    fn foo(&self) -> i32;
}
const _: () = {
    impl<'a, T: 'a + X + ?::core::marker::Sized> X for &'a mut T {
        fn foo(&self) -> i32 {
            T::foo(self)
        }
    }
};
const _: () = {
    extern crate alloc;
    impl<T: X + ?::core::marker::Sized> X for alloc::boxed::Box<T> {
        fn foo(&self) -> i32 {
            T::foo(self)
        }
    }
};
struct Q;
impl X for Q {
    fn foo(&self) -> i32 {
        42
    }
}
fn main() {
    let mut q = Q;
    (&q).foo();
    let z = &mut q;
    (z).foo();
}
