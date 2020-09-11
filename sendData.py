import azure
import json
from azure.eventhub import EventHubClient, Sender, EventData, Receiver, Offset
import threading
import datetime
import random

def send_data():
  EVENTHUBCONNECTIONSTRING = "<EventHubConnectionString>"
  EVENTHUBNAME = "<EventHubName>"

  telemetry_message = {
    "deviceid": "<Your device Id>",
    "timestamp": datetime.datetime.utcnow().isoformat(),
    "version" : "1",
    "sensors": [
      {
        "id": "<Your sensor Id>",
        "sensordata": [{
          "timestamp": datetime.datetime.utcnow().isoformat(),
          "Temperature": random.randint(150, 170) 
        }]
      }
    ]
  }

  write_client = EventHubClient.from_connection_string(EVENTHUBCONNECTIONSTRING, eventhub=EVENTHUBNAME, debug=True)
  sender = write_client.add_sender(partition="0")
  write_client.run()
  print("Sending telemetry: " + json.dumps(telemetry_message))
  sender.send(EventData(json.dumps(telemetry_message)))
  write_client.stop()

def setInterval(func,time):
    e = threading.Event()
    while not e.wait(time):
        func()

setInterval(send_data, 5)
# send_data()
