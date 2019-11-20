#!/bin/sh
#
# Releases package for Unity Package Manager.

tag="$1"

git subtree split --prefix=GenericPool.Unity/Assets/GenericPool.Unity ${tag} --squash -b release-package-unity/${tag}