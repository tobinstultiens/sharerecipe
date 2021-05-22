#!/bin/sh
cat keycloak-ingress.yaml | sed "s/KEYCLOAK_HOST/keycloak.$(minikube ip).nip.io/" | kubectl create -f -