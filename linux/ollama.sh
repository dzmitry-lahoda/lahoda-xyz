curl -fsSL https://ollama.com/install.sh | sh
ollama pull nomic-embed-text:latest
ollama pull llama3.1:8b
ollama pull gemma3:4b
ollama pull gemma3:12b

# autocomplete, tried `qwen2.5-coder:1.5b-base` - it is more stupid of ms gh copilot
ollama pull qwen2.5-coder:3b
ollama pull qwen2.5-coder:7b
ollama pull qwen2.5-coder:14b