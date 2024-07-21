enum Node<T> {
    Leaf(T),
    Left(T, Box<Node<T>>),
    Right(T, Box<Node<T>>),
    Both(T, Box<Node<T>>, Box<Node<T>>),
}

struct LeveledPayloadAugmentation<L> {
    level: L,
}

struct AvlNode<K, P, A> {
    children: [Option<Box<AvlNode<K, P,  A>>>; 2],
    key: K,
    augmentation: A,
    payload: P,
}

trait TreeConfig {
    type Tree;
    type Key;
    type Payload;
    type Augmentation;
}

impl<K, P, A,> AvlNode<K, P, A> {
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
        let left = self.children[0].as_ref().map(|x| x.height()).unwrap_or(-1);
        let right = self.children[1].as_ref().map(|x| x.height()).unwrap_or(-1);
        1 + std::cmp::max(left, right)
    }

    fn is_balanced(&self, disbalance: u8) -> bool {
        todo!()
    }

    // fn insert(&mut self, key: K, payload: P) -> Option<P> {
    //     // insert into binary tree
    //     match self.key.cmp(&key) {
    //         std::cmp::Ordering::Less => {
    //             if let Some(right) = &mut self.children[1] {
    //                 right.insert(key, payload)
    //             } else {
    //                 self.children[1] = Some(Box::new(AvlNode {
    //                     children: [None, None],
    //                     key,
    //                     augmentation: (),
    //                     payload,
    //                 }));
    //                 None
    //             }
    //         }
    //         std::cmp::Ordering::Greater => {
    //             if let Some(left) = &mut self.children[0] {
    //                 left.insert(key, payload)
    //             } else {
    //                 self.children[0] = Some(Box::new(AvlNode {
    //                     children: [None, None],
    //                     key,
    //                     augmentation: (),
    //                     payload,
    //                 }));
    //                 None
    //             }
    //         }
    //         std::cmp::Ordering::Equal => {
    //             let old_payload = std::mem::replace(&mut self.payload, payload);
    //             Some(old_payload)
    //         }
    //     }
        // None
    }
}

impl<K: core::cmp::PartialOrd, P, A> AvlNode<K, P, A> {
    /// has search property
    fn is_search(&self) -> bool {
        match (&self.children[0], &self.children[1]) {
            (None, Some(right)) => right.is_search() && self.key < right.key,
            (Some(left), None) => left.is_search() && self.key > left.key,
            (Some(left), Some(right)) => {
                left.is_search() && right.is_search() && self.key > left.key && self.key < right.key
            }
            (None, None) => true,
        }
    }
}

struct Tree<K, P, A> {
    root: AvlNode<K,P, A>,
    _marker: std::marker::PhantomData<(K, P, A)>,
}

fn main() {
    let tree = AvlNode::<_, _, ()> {
        children: [
            Some(Box::new(AvlNode {
                children: [None, None],
                key: 1,
                augmentation: (),
                payload: (),
            })),
            Some(Box::new(AvlNode {
                children: [None, None],
                key: 17,
                augmentation: (),
                payload: (),
            })),
        ],
        key: 10i32,
        augmentation: (),
        payload: (),
    };
    assert!(tree.is_search());
    assert_eq!(tree.height(), 1);
}
