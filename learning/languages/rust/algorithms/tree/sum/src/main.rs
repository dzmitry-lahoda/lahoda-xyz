struct AvlNode {
    left: Option<Box<AvlNode>>,
    right: Option<Box<AvlNode>>,
    value: i32,
    height: i32,
}


fn main() {
    println!("Hello, world!");
}
