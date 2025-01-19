/// Available Sealing methods.
#[cfg(feature = "manual-seal")]
#[derive(Debug, Copy, Clone, clap::ValueEnum)]
pub enum Sealing {
	// Seal using rpc method.
	Manual,
	// Seal when transaction is executed.
	Instant,
}

#[cfg(feature = "manual-seal")]
impl Default for Sealing {
	fn default() -> Sealing {
		Sealing::Manual
	}
}

#[allow(missing_docs)]
#[derive(Debug, clap::Parser)]
pub struct RunCmd {
	#[allow(missing_docs)]
	#[command(flatten)]
	pub base: sc_cli::RunCmd,

	/// Choose sealing method.
	#[cfg(feature = "manual-seal")]
	#[arg(long, value_enum, ignore_case = true)]
	pub sealing: Sealing,

	#[arg(long)]
	pub enable_dev_signer: bool,

	/// Maximum number of logs in a query.
	#[arg(long, default_value = "10000")]
	pub max_past_logs: u32,

	/// Maximum fee history cache size.
	#[arg(long, default_value = "2048")]
	pub fee_history_limit: u64,

	/// The dynamic-fee pallet target gas price set by block author
	#[arg(long, default_value = "1")]
	pub target_gas_price: u64,
}

#[derive(Debug, clap::Parser)]
pub struct Cli {
	#[command(subcommand)]
	pub subcommand: Option<Subcommand>,

	#[command(flatten)]
	pub run: RunCmd,
}

#[derive(Debug, clap::Subcommand)]
pub enum Subcommand {
	/// Key management cli utilities
	#[command(subcommand)]
	Key(sc_cli::KeySubcommand),

	/// DKG key management cli utilities
	#[command(subcommand)]
	DKGKey(dkg_primitives::dkg_key_cli::DKGKeySubcommand),

	/// Build a chain specification.
	BuildSpec(sc_cli::BuildSpecCmd),

	/// Validate blocks.
	CheckBlock(sc_cli::CheckBlockCmd),

	/// Export blocks.
	ExportBlocks(sc_cli::ExportBlocksCmd),

	/// Export the state of a given block into a chain spec.
	ExportState(sc_cli::ExportStateCmd),

	/// Import blocks.
	ImportBlocks(sc_cli::ImportBlocksCmd),

	/// Remove the whole chain.
	PurgeChain(sc_cli::PurgeChainCmd),

	/// Revert the chain to a previous state.
	Revert(sc_cli::RevertCmd),

	/// Sub-commands concerned with benchmarking.
	#[cfg(feature = "runtime-benchmarks")]
	#[command(subcommand)]
	Benchmark(frame_benchmarking_cli::BenchmarkCmd),

	/// Sub-commands concerned with benchmarking.
	#[cfg(not(feature = "runtime-benchmarks"))]
	Benchmark,

	/// Db meta columns information.
	#[cfg(feature = "testnet")]
	FrontierDb(fc_cli::FrontierDbCmd),
}
