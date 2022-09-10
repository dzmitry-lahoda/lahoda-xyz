fn variable<
    T, 
    G: 'static + Fn(T) -> Box<dyn Fn (Box<dyn Fn (T) -> T>) -> T>, 
    H: 'static + Fn (T) -> T
    >
(g: G, h:H) -> Box<dyn FnOnce(T) -> T> {
    let result = move |x|  g(x)(Box::new(move |y| h(y)));
    Box::new(result)
}


fn main() {
    let x = variable(
        |x| { Box::new(move |y| y(x*10))}, 
        |x| x+1 
    );
    let y = x(42);
    println!("Hello, world! {:#?}", y);
}
