enum Node<T> {
    Leaf(T),
    Left(T, Box<Node<T>>),
    Right(T, Box<Node<T>>),
    Both(T, Box<Node<T>>, Box<Node<T>>),
}

struct LeveledPayload<P, L> {
    payload: P,
    level: L,
}

struct AvlNode<K, M> {
    left: Option<Box<AvlNode<K, M>>>,
    right: Option<Box<AvlNode<K, M>>>,
    key: K,
    meta: M,
}

impl<K, M> AvlNode<K, M> {
    // fn new_leaf(key: K, payload: i32) -> Self {
    //     Self {
    //         left: None,
    //         right: None,
    //         key,
    //     }
    // }

    // fn new_root(key: K, value: i32, left: Self, right: Self) -> Self {
    //     Self {
    //         left: Some(Box::new(left)),
    //         right: Some(Box::new(right)),
    //         payload: value,
    //         key,
    //         level: 0,
    //     }
    // }

    fn height(&self) -> i32 {
        match (&self.left, &self.right) {
            (Some(left), Some(right)) => 1 + std::cmp::max(left.height(), right.height()),
            (Some(left), None) => 1 + left.height(),
            (None, Some(right)) => 1 + right.height(),
            (None, None) => 0,
        }
    }

    fn is_balanced(&self, disbalance: u8) -> bool {
        todo!()
    }
}

impl<K: core::cmp::PartialOrd, M> AvlNode<K, M> {
    /// has search property
    fn is_search(&self) -> bool {
        match (&self.left, &self.right) {
            (None, Some(right)) => right.is_search() && self.key < right.key,
            (Some(left), None) => left.is_search() && self.key > left.key,
            (Some(left), Some(right)) => {
                left.is_search() && right.is_search() && self.key > left.key && self.key < right.key
            }
            (None, None) => true,
        }
    }
}

struct Tree<K, M> {
    root: AvlNode<K, M>,
    _marker: std::marker::PhantomData<(K, M)>,
}

fn main() {
    let tree = AvlNode {
        left: Some(Box::new(AvlNode {
            left: None,
            right: None,
            key: 1,
            meta : (),
        })),
        right: Some(Box::new(AvlNode {
            left: None,
            right: None,
            key: 17,
            meta : (),
        })),
        key: 10i32,
        meta : (),
    };
    assert!(tree.is_search());
    assert_eq!(tree.height(), 1);
}
