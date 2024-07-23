use std::sync::Condvar;

enum BinaryNode<T> {
    Leaf(T),
    Left(T, Box<BinaryNode<T>>),
    Right(T, Box<BinaryNode<T>>),
    Both(T, Box<BinaryNode<T>>, Box<BinaryNode<T>>),
}

struct LeveledPayloadAugmentation<L> {
    level: L,
}

struct AvlNode<const C: usize, Config: TreeConfig> {
    children: [Option<Box<AvlNode<C, Config>>>; C],
    key: Config::Key,
    augmentation: Config::Augmentation,
    payload: Config::Payload,
}

trait KeyPayload {
    type Key;
    type Payload;
}

trait TreeConfig {
    type KeyValue: KeyValue;
    type Augmentation;
}

pub enum Modification<Config: TreeConfig> {
    Insert(Config::KeyPayload),
    Update(Config::KeyPayload, Config::KeyPayload),
    Delete(Config::KeyPayload),
}

trait AugmentationExtension<Config: TreeConfig> {
    /// Is called when leaf without children inserted
    fn leaf_inserted(&mut self, parent: Option<&Config::KeyValue>);
    fn child_modified(&mut self, parent: Option<&Config::KeyValue>, child: Modification<Config::KeyValue>);
}

impl<Config: TreeConfig> AvlNode<2, Config> {
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


    fn insert_entry(entry: Option<&mut Option<Box<AvlNode<2, Config>>>>, key: Config::Key) -> Option<Config::Key> {
        if let Some(Some(entry)) = entry {
            Some(std::mem::replace(&mut entry.key, key))
        } else {
         
            None
        }
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
    // }
}

impl<Config: TreeConfig<Augmentation = ()>> AvlNode<2, Config>
where
    Config::Key: std::cmp::Ord,
{


    fn insert(&mut self, key: Config::Key, payload: Config::Payload) { // -> Option<Config::Key> {
        use std::cmp::Ordering::*;
        match self.key.cmp(&key) {
            Less => {
                let entry = self.children.get_mut(1);
                match entry {
                    Some(Some(entry)) => entry.insert(key, payload),
                    Some(entry) => {
                        *entry = Some(Box::new(Self {
                            children: [None, None],
                            key,
                            augmentation: (),
                            payload,
                        }));
                    },
                    _=> unreachable!()
                }
            }
            Equal => {
                Some(std::mem::replace(&mut self.payload, payload));
                Some(std::mem::replace(&mut self.key, key));
                // augment
            },
            Greater => {
                let entry = self.children.get_mut(0);
                // Self::insert_entry(, key)
                // augment
            },
        }
    }

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

struct Tree<const C: usize, Config: TreeConfig> {
    root: AvlNode<C, Config>,
    _marker: std::marker::PhantomData<Config>,
}

struct IntegerAvlKeyonly;

impl TreeConfig for IntegerAvlKeyonly {
    // type Tree = Tree<Self>;
    type Key = i32;
    type Payload = ();
    type Augmentation = ();
}

fn main() {
    let tree = AvlNode::<2, IntegerAvlKeyonly> {
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
