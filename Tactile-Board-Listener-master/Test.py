import time
from UsbVibrationDevice import UsbVestDevice
import sys

if __name__ == "__main__":

    pin = int(sys.argv[1])
    print(str(pin))
    with UsbVestDevice("COM4") as driver:
        driver.set_frequency(0)
        driver.set_pin(pin, 1.0)
        time.sleep(1)