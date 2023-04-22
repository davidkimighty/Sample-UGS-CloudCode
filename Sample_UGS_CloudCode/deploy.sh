ugs config set environment-name development

# Remove the previous module
rm Sample_UGS_CloudCode.ccm

# Compress the contents of the directory and set the extension to .ccm
zip Sample_UGS_CloudCode.ccm *

# Deploy the module
ugs deploy Sample_UGS_CloudCode.ccm