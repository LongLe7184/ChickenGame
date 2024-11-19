void setup() {
    Serial.begin(9600);
}

void loop() {
    uint16_t xVal = analogRead(A0); //X values from Joystick
    uint16_t yVal = analogRead(A1); //Y values from Joystick
    bool button = digitalRead(8);  //Additional button - Try again  - PRESSED -> HIGH LOGIC

    /* Mapping and adjust center Joystick
     * 
     * When joystick is at the center position, the mapped values will be:
     * X default = 65 -> X ecpected = 0 (minus to 65)
     * Y default = 62 -> Y expected = 0 (minus to 62)
     */
    int8_t mappedX = map(xVal, 0, 1023, 127, 0) - 65;
    int8_t mappedY = map(yVal, 0, 1023, 0, 127) - 62;
    uint8_t buttons = 0x1 & button;

    /* 
     * USING ARDUINO DEFAULT CONFIG FOR A UART FRAME - Serial.print
     * Start Bit: 0
     * Data Bits: 8 bits (the actual data)
     * Parity Bit: None
     * Stop Bit: 1
    */
     
    //Using " " to splitting the frame so the Winform App can detect it
    Serial.print(mappedX);
    Serial.print(" ");  
    Serial.print(mappedY);
    Serial.print(" ");
    Serial.print(buttons);
    Serial.println();

    //With baud rate = 9600, it tooks 6.25ms 
    delay(50);
    //After 66.25ms, the next sequence will be send 
}
