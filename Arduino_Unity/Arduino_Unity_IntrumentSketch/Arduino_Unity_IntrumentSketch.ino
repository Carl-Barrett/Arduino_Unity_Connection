const int buttonPins[] = {2, 3, 4, 5, 6}; // The pins for the buttons
const int noteFSharp = 370; // The frequency for F#
const int noteGSharp = 415; // The frequency for G#
const int noteA = 440; // The frequency for A
const int noteCSharp = 554; // The frequency for C#
const int noteE = 659; // The frequency for E
const char buttonChars[] = {'F', 'G', 'A', 'C', 'E'}; // The characters to send to Unity for each button

void setup() {
  Serial.begin(9600); // Start the serial connection
  for (int i = 0; i < 5; i++) {
    pinMode(buttonPins[i], INPUT_PULLUP); // Set the button pins as inputs with internal pull-up resistors
  }
}

void loop() {
  for (int i = 0; i < 5; i++) {
    if (digitalRead(buttonPins[i]) == LOW) { // If the button is pressed
      tone(9, getNoteFrequency(i)); // Play the corresponding note on the buzzer
      sendButtonToUnity(buttonChars[i]); // Send the corresponding button character to Unity
      delay(50); // Delay to avoid multiple tones from one press
      noTone(9);
    }
  }
}

int getNoteFrequency(int buttonIndex) {
  switch (buttonIndex) { // Determine which button was pressed
    case 0: // Button connected to pin 2 (F#)
      return noteFSharp;
    case 1: // Button connected to pin 3 (G#)
      return noteGSharp;
    case 2: // Button connected to pin 4 (A)
      return noteA;
    case 3: // Button connected to pin 5 (C#)
      return noteCSharp;
    case 4: // Button connected to pin 6 (E)
      return noteE;
    default:
      return 0;
  }
}

void sendButtonToUnity(char buttonChar) {
  Serial.write(buttonChar); // Send the button to Unity
}





