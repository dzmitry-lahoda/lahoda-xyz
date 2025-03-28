if command -v glxinfo > /dev/null; then
  glxinfo
fi

if command -v lspci > /dev/null; then
  lspci | grep VGA
fi

if command -v lshw > /dev/null; then
  lshw -class display
fi

if command -v xrandr > /dev/null; then
  xrandr --listmonitors
fi

if command -v nvidia-smi > /dev/null; then
  nvidia-smi
fi

if command aticonfig > /dev/null; then
  aticonfig --odgt
fi