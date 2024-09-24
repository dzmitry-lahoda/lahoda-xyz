use core::fmt::Debug;
use std::any::Any;

use augumented_trii::core::*;

enum BinaryNode<T> {
    Leaf(T),
    Left(T, Box<BinaryNode<T>>),
    Right(T, Box<BinaryNode<T>>),
    Both(T, Box<BinaryNode<T>>, Box<BinaryNode<T>>),
}

struct LeveledPayloadAugmentation<L> {
    level: L,
}

pub struct AvlNode<const C: usize, Config: TreeConfig> {
    children: [Option<Box<AvlNode<C, Config>>>; C],
    key: <Config::KeyPayload as KeyPayload>::Key,
    augmentation: Config::Augmentation,
    payload: <Config::KeyPayload as KeyPayload>::Payload,
}

impl<Config: TreeConfig<KeyPayload: KeyPayload<Payload : Debug + 'static> + Debug, Augmentation : Debug + 'static>> Debug for AvlNode<2, Config>
where
    <<Config as TreeConfig>::KeyPayload as KeyPayload>::Key: Debug,
{
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        let mut debug_struct = f.debug_struct("AvlNode");            
        debug_struct.field("key", &self.key);
        if self.augmentation.type_id() != ().type_id()
        {
            debug_struct.field("augmentation", &self.augmentation);
        }
        if self.payload.type_id() != ().type_id() {
            debug_struct.field("payload", &self.payload);
        }
        if let Some(left) = &self.children[0] {
            debug_struct.field("left", left);
        }        
        if let Some(right) = &self.children[1] {
            debug_struct.field("right", right);
        }
        debug_struct.finish()
    }
}

// impl<Config: TreeConfig<KeyPayload: Debug, Augmentation: Debug>> Debug for AvlNode<2, Config>
// where
//     <<Config as TreeConfig>::KeyPayload as KeyPayload>::Key: Debug,
//     <<Config as TreeConfig>::KeyPayload as KeyPayload>::Payload: Debug,
// {
//     fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
//         f.debug_struct("AvlNode")
//             .field("children", &self.children)
//             .field("key", &self.key)
//             .field("augmentation", &self.augmentation)
//             .field("payload", &self.payload)
//             .finish()
//     }
// }

#[derive(Debug)]
pub enum Modification<Config: TreeConfig> {
    // If key and payload was freshly inserted as child.
    Insert(Config::KeyPayload),
    // If key and payload was updated in existing node.
    Update(Config::KeyPayload, Config::KeyPayload),
    // If key and payload was removed from parent.
    Delete(Config::KeyPayload),
}

#[allow(type_alias_bounds)]
type Key<Config: TreeConfig> = <Config::KeyPayload as KeyPayload>::Key;
#[allow(type_alias_bounds)]
type Payload<Config: TreeConfig> = <Config::KeyPayload as KeyPayload>::Payload;

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

    /// Does not have children.
    fn is_leaf(&self) -> bool {
        self.children.iter().all(Option::is_none)
    }
}

impl<Config: TreeConfig<Augmentation = ()>> AvlNode<2, Config>
where
    <<Config as TreeConfig>::KeyPayload as KeyPayload>::Key: Ord + PartialOrd,
{
    pub fn remove(&mut self, key: Key<Config>) {
        use std::cmp::Ordering::*;
        
        if let Some(entry) = self.children.get_mut(0).unwrap() {
            if entry.key == key {
                if entry.is_leaf() {
                    *entry = None;
                } else {
                    
                }
            }
        }

        // for child in self.children.iter_mut() {
        //     if let Some(child) = child {
        //         if child.key == key {
        //             if child.is_leaf() {
        //                 *child = None;
        //             } else {
        //                 if
        //             }
        //         }
        //     }
        // }
        // // for child in self.children {
        // //     if let Some(Some(child)) = child {
        // //         if child.key == key {
        // //             // remove
        // //         }
        // //     }
        // // }

        // match self.key.cmp(&key) {
        //     Less => {
        //         let entry = self.children.get_mut(1);
        //         if let Some(Some(entry)) = entry {
        //             Self::remove(entry, key);
        //             // augument
        //         }
        //     }
        //     Equal => {
        //         // remove
        //     }
        //     Greater => {
        //         let entry = self.children.get_mut(0);
        //         if let Some(Some(entry)) = entry {
        //             Self::remove(entry, key);
        //         }
        //     }
        // }
    }

    pub fn insert(&mut self, key: Key<Config>, payload: Payload<Config>) {
        // -> Option<Config::Key> {
        use std::cmp::Ordering::*;
        match self.key.cmp(&key) {
            Less => {
                let entry = self.children.get_mut(1);
                Self::insert_entry(entry, key, payload);
            }
            Equal => {
                Some(std::mem::replace(&mut self.payload, payload));
                Some(std::mem::replace(&mut self.key, key));
                // augment
            }
            Greater => {
                let entry = self.children.get_mut(0);
                Self::insert_entry(entry, key, payload);

                // augment
            }
        }
    }

    fn insert_entry(
        entry: Option<&mut Option<Box<AvlNode<2, Config>>>>,
        key: <<Config as TreeConfig>::KeyPayload as KeyPayload>::Key,
        payload: <<Config as TreeConfig>::KeyPayload as KeyPayload>::Payload,
    ) {
        match entry {
            Some(Some(entry)) => entry.insert(key, payload),
            Some(entry) => {
                *entry = Some(Box::new(Self {
                    children: [None, None],
                    key,
                    augmentation: Config::Extension::leaf_inserted(),
                    payload,
                }));
            }
            _ => unreachable!(),
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

#[derive(Debug)]
struct IntegerAvlKeyonly;

/// For benchmarking with set.
impl TreeConfig for IntegerAvlKeyonly {
    type KeyPayload = (i32, ());
    type Augmentation = ();

    type Extension = ();
}

fn main() {
    let mut tree = AvlNode::<2, IntegerAvlKeyonly> {
        key: 10i32,
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
        augmentation: (),
        payload: (),
    };
    assert!(tree.is_search());
    assert_eq!(tree.height(), 1);
    tree.insert(5, ());
    assert!(tree.is_search());
    println!("tree: {:#?}", tree);
    tree.remove(10);
}
