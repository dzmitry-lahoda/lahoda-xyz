Rust has very flexible visiblity options:
- https://doc.rust-lang.org/reference/visibility-and-privacy.html and https://rust-lang.github.io/api-guidelines/future-proofing.html
- https://predr.ag/blog/definitive-guide-to-sealed-traits-in-rust/
- https://doc.rust-lang.org/cargo/reference/features.html with https://doc.rust-lang.org/reference/conditional-compilation.html
- `hidden` https://doc.rust-lang.org/rustdoc/write-documentation/the-doc-attribute.html and track_caller attribute 
- name prefix `__`

Features has language level and crate level tooling:
- https://crates.io/crates/sealed
- static assertions
- https://github.com/taiki-e/negative-impl
- some other crate I lost which prevents impl some traits at compile time (like if I do not want Clone or Copy)

Depending on case, some are better of others. 
