from MqttMessageService import MqttMessageService
from VibrationPatternPlayer import VibrationPatternPlayer
#from VestDeviceBase import DummyVestDevice
from HaptogramService import HaptogramService
#from I2CVibrationDevice import I2CVestDevice
from UsbVibrationDevice import UsbVestDevice
import json
import time

class TactileBoardListener:
    def __init__(self, message_bus, haptogram_service):
        self.hs = haptogram_service
        message_bus.add_listener("happify/play", self._handle_message)
    
    def _handle_message(self, data):
        payload = json.loads(data.payload)
        self.hs.enqueue(payload)
        return

if __name__ == "__main__":
    with UsbVestDevice("COM4") as vest_device, MqttMessageService() as mb:
        vpp = VibrationPatternPlayer(vest_device)
        hs = HaptogramService(vpp, 2.0, 0.1)
        hs.start()
        listener = TactileBoardListener(mb, hs)

        #while True:
            #pass

        while True:
            time.sleep(1)

        hs.stop()