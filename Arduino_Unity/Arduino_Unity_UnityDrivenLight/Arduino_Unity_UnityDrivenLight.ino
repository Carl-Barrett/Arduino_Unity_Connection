#include <DHT.h>

#define DHTPIN 2
#define DHTTYPE DHT11

DHT dht(DHTPIN, DHTTYPE);

void setup() {
  // initialize the serial communication
  Serial.begin(9600);

  // initialize the DHT sensor
  dht.begin();
}

void loop() {
  // read the humidity and temperature data from the DHT sensor
  float humidity = dht.readHumidity();
  float temperature = dht.readTemperature();

  // check if the data is valid
  if (!isnan(humidity) && !isnan(temperature)) {
   Serial.print("H:");
  Serial.print(humidity, 2); // print humidity with 2 decimal places
  Serial.print(",");

  Serial.print("T:");
  Serial.println(temperature, 2); // print temperature with 2 decimal places
  }

  // wait for a short time before reading the data again
  delay(1000);
}



