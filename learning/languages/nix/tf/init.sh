source ./.tfvars
printf $PROJECT
gcloud auth login
gcloud auth application-default login
gcloud projects create $PROJECT --set-as-default
gcloud config set project $PROJECT
