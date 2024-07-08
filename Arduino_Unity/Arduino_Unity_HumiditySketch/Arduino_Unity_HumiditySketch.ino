#include <DHT.h>

#define DHTPIN 2
#define DHTTYPE DHT11
#define redPin 4
#define greenPin 5
#define bluePin 6

DHT dht(DHTPIN, DHTTYPE);

void setup() {
// initialize the serial communication
Serial.begin(9600);

// initialize the RGB pins
pinMode(redPin, OUTPUT);
pinMode(greenPin, OUTPUT);
pinMode(bluePin, OUTPUT);

// initialize the DHT sensor
dht.begin();
}

void loop() {
// read the humidity and temperature data from the DHT sensor
float humidity = dht.readHumidity();
float temperature = dht.readTemperature();

// check if the data is valid
if (!isnan(humidity) && !isnan(temperature)) {
// print the humidity and temperature data
Serial.print("H:");
Serial.print(humidity, 2); // print humidity with 2 decimal places
Serial.print(",");
Serial.print("T:");
Serial.println(temperature, 2); // print temperature with 2 decimal places
// set the RGB values based on the temperature
if (temperature > 25) {
  // set the LED to red
  digitalWrite(redPin, HIGH);
  digitalWrite(greenPin, LOW);
  digitalWrite(bluePin, LOW);
} else if (temperature < 20) {
  // set the LED to blue
  digitalWrite(redPin, LOW);
  digitalWrite(greenPin, LOW);
  digitalWrite(bluePin, HIGH);
} else {
  // set the LED to green
  digitalWrite(redPin, LOW);
  digitalWrite(greenPin, HIGH);
  digitalWrite(bluePin, LOW);
}
}

// wait for a short time before reading the data again
delay(1000);
}
