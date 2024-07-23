//! Shared interfaces, types and composable algorithms for the augumented trees and tries.


pub trait KeyPayload {
    type Key;
    type Payload;
}

pub trait TreeConfig {
    type KeyPayload: KeyPayload;
    type Augmentation;
    type Extension: AugmentationExtension<Self::Augmentation>;
}


pub trait AugmentationExtension<Augmentation> {
    /// Is called when leaf without children inserted.
    /// Returns new augmentation for `parent`.
    fn leaf_inserted(//parent: Option<&Config::KeyPayload>
    ) -> Augmentation;
    /// Returns new augmentation for `parent`.
    fn child_modified(//parent: Option<&Config::KeyPayload>, child: Modification<Config>
    ) -> Augmentation;
}

/// No augmentation.
impl AugmentationExtension<()> for () {
    #[inline]

    fn leaf_inserted(//parent: Option<&Config::KeyPayload>
    ) -> () {
        ()
    }
    #[inline]

    fn child_modified(//parent: Option<&Config::KeyPayload>, child: Modification<Config>
    ) -> () {
        ()
    }
}

impl<K, P> KeyPayload for (K, P) {
    type Key = K;
    type Payload = P;
}
