void setup() {
    Serial.begin(9600);
}

void loop() {
    int xVal = analogRead(A0);
    int yVal = analogRead(A1);
    int outX, outY;
    if(0<=xVal && xVal<=342){
      outX = 1;
    } else if (343<=xVal && xVal<=684) {
      outX = 0;  
    } else {
      outX = -1;  
    }
    if(0<=yVal && yVal<=342){
      outY = -1;
    } else if (343<=yVal && yVal<=684) {
      outY = 0;  
    } else {
      outY = 1;  
    }
    Serial.print(outX);
    Serial.print(" ");
    Serial.println(outY);
    delay(80);
}
