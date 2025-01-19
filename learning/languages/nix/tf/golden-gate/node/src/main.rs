//! Substrate Node Template CLI library.

#![warn(missing_docs)]
#![allow(clippy::type_complexity, clippy::too_many_arguments)]

#[cfg(all(feature = "testnet", feature = "mainnet"))]
compile_error!("feature \"testnet\" and feature \"mainnet\" cannot be enabled at the same time");
