import requests
import json
import msal

# Your service principal App ID
CLIENT_ID = "<Your app registration client id>"
# Your service principal password
CLIENT_SECRET = "<Your app registration client secret>"
# Tenant ID for your Azure subscription
TENANT_ID = "<your Azure Active Directory tenant id>"

AUTHORITY_HOST = 'https://login.microsoftonline.com'
AUTHORITY = AUTHORITY_HOST + '/' + TENANT_ID

ENDPOINT = "<Your datahub endpoint>"
SCOPE = ENDPOINT + "/.default"

context = msal.ConfidentialClientApplication(CLIENT_ID, authority=AUTHORITY, client_credential=CLIENT_SECRET)
token_response = context.acquire_token_for_client(SCOPE)
# We should get an access token here
access_token = token_response.get('access_token')
# print("RESPONSE : " + str(token_response))
print("ACESS TOKEN : " + str(access_token))