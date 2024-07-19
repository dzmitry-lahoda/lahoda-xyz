struct AvlNode {
    left: Option<Box<AvlNode>>,
    right: Option<Box<AvlNode>>,
    key: i32,
    value: i32,
    height: i32,
}

impl AvlNode {
    fn new_leaf(key: i32, value: i32) -> AvlNode {
        AvlNode {
            left: None,
            right: None,
            value,
            key,
            height: 0,
        }
    }

    fn new_root(key: i32, value: i32, left: AvlNode, right: AvlNode) -> AvlNode {
        AvlNode {
            left: Some(Box::new(left)),
            right: Some(Box::new(right)),
            value,
            key,
            height: 0,
        }
    }

    /// has search property
    fn is_search(&self) -> bool{
        match (&self.left, &self.right) {
            (None, None) => true,
            (None, Some(right)) => right.is_search() && self.key < right.key,
            (Some(left), None) => left.is_search() && self.key > left.key,
            (Some(left), Some(right)) => left.is_search() && right.is_search() && self.key > left.key && self.key < right.key,
        }
        // let left = self.left.map(|x| x.is_search()).unwrap_or(true);
        // let right = self.right.map(|x| x.is_search()).unwrap_or(true);
    }

    fn is_balances(&self, disbalance: u8) -> bool {
        todo!()
    }
}

struct Tree {
    root: AvlNode,
}

fn main() {
    let tree = AvlNode {
        left: Some(Box::new(AvlNode {
            left: None,
            right: None,
            key: 1,
            value: 13,
            height: 1,    
        })),
        right: Some(Box::new(AvlNode {
            left: None,
            right: None,
            key: 17,
            value: 42,
            height: 1,    
        })),
        key: 10,
        value: 2,
        height: 0,
    };
    assert!(tree.is_search());
}
