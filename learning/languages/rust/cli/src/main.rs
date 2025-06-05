
use clap::*;
use clap::parser::*;

#[derive(Parser, Debug)]
pub struct VaultTransactionCreate {
    #[arg(long)]
    pub transaction_message: Vec<u8>,
}
fn main() {
    let clap = VaultTransactionCreate::parse(); 
    println!("{:?}", clap);
}
