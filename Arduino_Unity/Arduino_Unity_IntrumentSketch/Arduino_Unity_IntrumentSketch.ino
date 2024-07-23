const int buttonPins[] = {2, 3, 4, 5, 6}; // The pins for the buttons
const int noteFSharp = 370; 
const int noteGSharp = 415; 
const int noteA = 440; 
const int noteCSharp = 554; 
const int noteE = 659; 
const char buttonChars[] = {'F', 'G', 'A', 'C', 'E'}; // The characters to send to Unity for each button

void setup() {
  Serial.begin(9600); 
  for (int i = 0; i < 5; i++) {
    pinMode(buttonPins[i], INPUT_PULLUP); 
  }
}

void loop() {
  for (int i = 0; i < 5; i++) {
    if (digitalRead(buttonPins[i]) == LOW) { 
      tone(9, getNoteFrequency(i)); 
      sendButtonToUnity(buttonChars[i]); 
      delay(50); // Delay to avoid multiple tones from one press
      noTone(9);
    }
  }
}

int getNoteFrequency(int buttonIndex) {
  switch (buttonIndex) { 
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





