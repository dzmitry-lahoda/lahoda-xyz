

com::interfaces! {
    #[uuid("00000000-0000-0000-C000-000000000046")]
    pub unsafe interface IUnknown {
        fn QueryInterface(
            &self,
            riid: *const IID,
            ppv: *mut *mut c_void
        ) -> HRESULT;
        fn AddRef(&self) -> u32;
        fn Release(&self) -> u32;
    }    
}