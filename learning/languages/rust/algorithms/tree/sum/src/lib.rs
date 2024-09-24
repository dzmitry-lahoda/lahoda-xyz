//! Very abstract library to compose trees and tries with various features and implementations.
//! 
//! Supports:
//! - just key
//! - key and payload(value)
//! - augmentations (on modification with change in parent and child data accesible)
//! - paramterized storage and access patterns (index/ref/box/etc, arc/rc/mutex/etc)
//! - extensible traversals (including `log n`` random)
//! - custom node strategies (can have various special nodes explicitly designed)
//! - differrent balancing rules
pub mod core;
