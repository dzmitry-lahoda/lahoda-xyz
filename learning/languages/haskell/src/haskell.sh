set -o errexit -o errtrace -o pipefail
ghc $1.hs 
chmod +x $1
./$1