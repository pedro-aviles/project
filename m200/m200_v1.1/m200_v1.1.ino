//#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>
#include <TimerOne.h>
#include <EEPROM.h>

#define SCREEN_WIDTH 128                // OLED display width
#define SCREEN_HEIGHT 64                // OLED display height
#define REC_LENGTH 200                  // Waveform data buffer size
#define MIN_TRIG_SWING 5                // Minimum trigger swing (Unsybc display if below this amplitude)
#define Samples 5

#define logicProbe_Pin 3                // for logic probe and frecuency measurments  PD3
#define blueLed_Pin 5                   //blue led                                    PD5
#define slect_Pin 6                     //slect button                                PD6
#define plus_Pin  7                     //plus button                                 PD7

#define mode_Pin  8                    //mode buton                                   PB0
#define resistor_6p8K_pin 9            // Enable or High Z pin for resistor 6.8K Attenuating    PB1
#define resistor_3p3K_pin 10           // Enable or High Z pin for resistor 3.3K Attenuating    PB2
#define powerLed_Pin 11                // enable power led                            PB3
#define buzzer_Pin 12                  //buzzer enable pin                            PB4
#define redLed_Pin  13                 //red led                                      PB5

#define adjust_Pin A2                  //Pin for adjust menu                          PC2
#define scope_Pin A6                   // ADC pin for voltage measure                 ADC6
#define current_Pin A7                 // ADC pin for current (I) measure             ADC7

//#define debug_Pin  A0                   //debug led                                 PC0
//#define mosfetEnable_Pin  11            //mosfet that enable current pass throu     PB3
//#define vibrator_Pin  12                //vibrator enable                           PB4


static const unsigned char PROGMEM speak24[] = {
  0x00, 0x30, 0x00, 0x00, 0x70, 0x00, 0x00, 0xf0, 0x18, 0x03, 0xf0, 0x1c, 0x07, 0xf0, 0x0c, 0x07, 0xf0, 0xc6, 0xe7, 0xf0, 0xc6, 0xe7, 0xf0, 0x63,
  0xe7, 0xf4, 0x63, 0xe7, 0xf6, 0x33, 0xe7, 0xf2, 0x33, 0xe7, 0xf3, 0x33, 0xe7, 0xf3, 0x33, 0xe7, 0xf2, 0x33, 0xe7, 0xf6, 0x33, 0xe7, 0xf4, 0x63,
  0xe7, 0xf0, 0x63, 0xe7, 0xf0, 0xc6, 0x07, 0xf0, 0xc6, 0x07, 0xf0, 0x0c, 0x03, 0xf0, 0x1c, 0x00, 0xf0, 0x18, 0x00, 0x70, 0x00, 0x00, 0x30, 0x00
};
static const unsigned char PROGMEM noSpeak24[] = {
  0x00, 0x30, 0x00, 0x00, 0x70, 0x00, 0x00, 0xf0, 0x00, 0x03, 0xf0, 0x00, 0x07, 0xf0, 0x00, 0x07, 0xf0, 0x00, 0xe7, 0xf6, 0x03, 0xe7, 0xf7, 0x07,
  0xe7, 0xf3, 0x8e, 0xe7, 0xf1, 0xdc, 0xe7, 0xf0, 0xf8, 0xe7, 0xf0, 0x70, 0xe7, 0xf0, 0xf8, 0xe7, 0xf1, 0xdc, 0xe7, 0xf3, 0x8e, 0xe7, 0xf7, 0x07,
  0xe7, 0xf6, 0x03, 0xe7, 0xf0, 0x00, 0x07, 0xf0, 0x00, 0x07, 0xf0, 0x00, 0x03, 0xf0, 0x00, 0x00, 0xf0, 0x00, 0x00, 0x70, 0x00, 0x00, 0x30, 0x00
};

//************voltimiter/logic probe Variables**************//

float voltage_Adjust_COEF = 0;
float current_Adjust_COEF = 0;
float current_Adjust_SUM = 0;
float y, u = 0; // voltage filter
float x = 540;
float z = 540; // current filter

byte previousV = 0;        //for buzzer purpose

volatile byte Rotation = 0;   //0= derecho, 2= izquierdo
volatile byte Function = 0;   //0= voltimeter/punta logica, 1 = transition ,2 Scope, 3 transition

volatile byte vRange = 1;         // Vertical range  0: 2.5V, 1: 5V, 2: 10V, 3: 25V
volatile byte hRange = 1;         // Horizontal range 0: 50m, 1: 20m, 2: 10m, 3: 5m, 4; 2m, 5: 1m, 6: 500u, 7; 200u
volatile byte scopeP = 1;         // Operation scope position 0: Vertical range, 1: Horizontal range, 2: Trigger direction
volatile byte hold = 0; // hold

volatile unsigned int timerCounter1sec = false; // hold
volatile unsigned int timerCounter100mS = 0; //

//************voltimiter/logic probe funtions**************//
void Voltimiter_logicProbe();
void startTimer2();
void logicMeasure();
void currentMeasure();
void nope(void);
void Adjust();
void BuzzerToggle();
void buzzerNvibrator(void);


//************scope Variables**************//
volatile unsigned long counter = 0;
volatile unsigned long counterBackup = 0;
volatile int measureRange = 0;

volatile boolean switchPushed = false; // The switch has been pressed! flag

int dataMin;                   // Minimum value of buffer (min: 0)
int dataMax;                   // Maximum value of the buffer (max: 1023)
int dataAve;                   // Average value of buffer (stored 10 times to ensure accuracy max: 10230)
int rangeMax;                  // Buffer value that makes the graph full scale
int rangeMin;                   // value of the buffer that lowers the graph
int rangeMaxDisp;              // Max display value (specified by 100 times)
int rangeMinDisp;              // min display value
int trigP;                     // Trigger position on the data buffer
boolean trigSync;               // Trigger detection flag
bool att10x = 0;                  // Input attenuator (valid at 1)


// Save range display name to flash memory
const char vRangeName[5][5] PROGMEM = {"2V5", "5V", "10V", "25V"}; // Vertical axis display character (requires the number of characters including \ 0)
const char * const vstring_table[] PROGMEM = {vRangeName[0], vRangeName[1], vRangeName[2], vRangeName[3]};
const char hRangeName[10][6] PROGMEM = {"200mS", "100mS", " 50mS", " 20mS", " 10mS", "  5mS", "  2mS", "  1mS", "500uS", "200uS"}; // Horizontal axis display character (60 bytes)
const char * const hstring_table[] PROGMEM = {hRangeName[0], hRangeName[1], hRangeName[2], hRangeName[3], hRangeName[4], hRangeName[5], hRangeName[6], hRangeName[7], hRangeName[8], hRangeName[9]};

int waveBuff[REC_LENGTH];       // Waveform data recording memory (RAM is barely available)
char chrBuff[8];                // Display format buffer
char hScale[] = "xxxAs";        // horizontal scale name
char vScale[] = "xxxx";        // vertical scale name


//************scope funtions**************//
void scope(void);
void setConditions(void);                          // Set measurement conditions
void readWave(void);                                // Waveform reading (1.6-400ms)
void setConditions(void);                           // Reset measurement conditions (reflect changes in measurement on display)
void dataAnalize(void);                             // Collect various data information (0.5-0.6ms)
void writeCommonImage(void);                       // Drawing a fixed image (2.6ms)
void plotData(void);                               // Waveform plot (10-18ms)
void dispInf(void);
void dispHold(void);
void Safe(void);


// Declaration for an SSD1306 display connected to I2C (SDA, SCL pins)
Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, -1);

void setup() {

  //check for lcd // Address 0x3C for 128x64
  if (!display.begin(SSD1306_SWITCHCAPVCC, 0x3C)) while (true); // Don't proceed, loop forever

  display.clearDisplay();
  display.setTextSize(4);
  display.setTextColor(WHITE);
  display.setCursor(0, 20);
  display.println(F("mBeta"));
  display.display();

  analogReference(INTERNAL);                // Set ADC full scale to 1.1V (use internal vref)

  pinMode(buzzer_Pin, OUTPUT);              //buzzer pin to low
  digitalWrite(buzzer_Pin, LOW);
  pinMode(logicProbe_Pin, INPUT);             //for frecuency mesurment and logic probe
  pinMode(blueLed_Pin, OUTPUT);               // blu led indicate gnd
  digitalWrite(blueLed_Pin, LOW);
  pinMode(redLed_Pin, OUTPUT);                //red led indicate positive
  digitalWrite(redLed_Pin, LOW);
  //pinMode(mosfetEnable_Pin, OUTPUT);                //mosfet that enable current measurment
 // digitalWrite(mosfetEnable_Pin, HIGH);             //apagar mosfet
  //pinMode(vibrator_Pin, OUTPUT);                //vibrator mosfet
  //digitalWrite(vibrator_Pin, LOW);             //apagar vibrador
  pinMode(adjust_Pin, INPUT_PULLUP);
  pinMode(powerLed_Pin, OUTPUT);              //pin for power led
  digitalWrite(powerLed_Pin, LOW);
  pinMode(resistor_6p8K_pin, INPUT);              //input High Z so resistor is not in series to 100K
  pinMode(resistor_3p3K_pin, OUTPUT);              //output so resistor can connect to GND
  digitalWrite(resistor_3p3K_pin, LOW);            //resistor is in series to 100K
  pinMode(slect_Pin, INPUT_PULLUP);
  pinMode(plus_Pin, INPUT_PULLUP);
  pinMode(mode_Pin, INPUT_PULLUP);

//  pinMode(debug_Pin, OUTPUT);
//  digitalWrite(debug_Pin, LOW);

//  pinMode(fromOPAMP, INPUT);
//   attachInterrupt(digitalPinToInterrupt(2), Safe, FALLING ); //for frecuency measurments

  if (!bitRead(PINC, 2)) Adjust();

  //30V coeficient Adjust

  byte byte0 = EEPROM.read(0);
  byte byte1 = EEPROM.read(1);
  byte signo = 0; //0=positivo 1= negativo
  if (bitRead(byte1, 7)) {
    bitClear(byte1, 7); //si el numero es negativo restar la cantidad quitar el signo del numero
    signo = 1;
  }
  unsigned int value = byte1 << 8;
  value = value + byte0;
  voltage_Adjust_COEF = value;
  voltage_Adjust_COEF = voltage_Adjust_COEF / 1000000;
  if (signo)voltage_Adjust_COEF = voltage_Adjust_COEF * (-1);
  voltage_Adjust_COEF = 0.033659 - voltage_Adjust_COEF;

  display.clearDisplay();
  display.setTextSize(1);
  display.setCursor(0, 20);


  //10A coeficient Adjust
  byte0 = EEPROM.read(2);
  byte1 = EEPROM.read(3);
  signo = 0; //0=positivo 1= negativo
  if (bitRead(byte1, 7)) {
    bitClear(byte1, 7); //si el numero es negativo restar la cantidad quitar el signo del numero
    signo = 1;
  }
  value = byte1 << 8;
  value = value + byte0;
  current_Adjust_COEF = value;

  current_Adjust_COEF = current_Adjust_COEF / 1000000;
  if (signo)current_Adjust_COEF = current_Adjust_COEF * (-1);

  current_Adjust_COEF = 0.023118 - current_Adjust_COEF;
  //display.println(current_Adjust_COEF * 10000);

  //10A Sum Adjust
  byte0 = EEPROM.read(4);
  byte1 = EEPROM.read(5);
  signo = 0; //0=positivo 1= negativo
  if (bitRead(byte1, 7)) {
    bitClear(byte1, 7); //si el numero es negativo restar la cantidad quitar el signo del numero
    signo = 1;
  }
  value = byte1 << 8;
  value = value + byte0;
  current_Adjust_SUM = value;


  current_Adjust_SUM = current_Adjust_SUM / 1000;
  if (signo)current_Adjust_SUM = current_Adjust_SUM * (-1);
  current_Adjust_SUM = 12.5 - current_Adjust_SUM;


  Timer1.initialize(1000000);
  Timer1.attachInterrupt(BuzzerToggle);
  digitalWrite(buzzer_Pin, LOW);
  Timer1.stop();

  digitalWrite(buzzer_Pin, HIGH);
 // digitalWrite(vibrator_Pin, HIGH);
  delay(10);
  digitalWrite(buzzer_Pin, LOW);
  delay(100);
  digitalWrite(buzzer_Pin, HIGH);
  delay(50);
  digitalWrite(buzzer_Pin, LOW);
//  digitalWrite(vibrator_Pin, LOW);
  delay(1000);
  startTimer2();                            // star timer 2


}

void loop() {

  switch (Function) {
    case 0:
      Voltimiter_logicProbe();
      break;
    case 1:
      buzzerNvibrator();
      Function++;
      break;
    case 2:
      scope();
      break;
    case 3:
      buzzerNvibrator();
      Function = 0;
      break;
    default:
      // statements
      break;
  }
}

void buzzerNvibrator(void) {
  digitalWrite(buzzer_Pin, HIGH);
//  digitalWrite(vibrator_Pin, HIGH);
  delay(20);
  digitalWrite(buzzer_Pin, LOW);
  delay(100);
//  digitalWrite(vibrator_Pin, LOW);
}

void BuzzerToggle(void) {
  digitalWrite(buzzer_Pin, !digitalRead(buzzer_Pin));
}

void Adjust() {
  display.clearDisplay();
  display.setTextSize(2);
  display.setCursor(0, 20);
  display.println(F("3"));
  display.display();
  delay(1000);

  display.clearDisplay();
  display.setCursor(0, 20);
  display.println(F("2"));
  display.display();
  delay(1000);

  display.clearDisplay();
  display.setCursor(0, 20);
  display.println(F("1"));
  display.display();
  delay(1000);

  if (bitRead(PINC, 2))  {

    display.clearDisplay();
    display.setTextSize(2);
    display.setCursor(0, 20);
    display.println(F("V?3"));
    display.display();
    delay(1000);

    display.clearDisplay();
    display.setCursor(0, 20);
    display.println(F("V?2"));
    display.display();
    delay(1000);

    display.clearDisplay();
    display.setCursor(0, 20);
    display.println(F("V?1"));
    display.display();
    delay(1000);
    if (!bitRead(PINC, 2)) {//adjust voltage

      float sumVoltage = 0;
      byte byte0 = 0;
      byte byte1 = 0;
      byte signo = 0;

      for (unsigned int i = 0; i < 20000; i++) {//sumVoltage = analogRead(scope_Pin) + sumVoltage;
        u = analogRead(scope_Pin);
        y = 0.9391 * y + 0.0609 * u;
      }
      sumVoltage = y;//sumVoltage = sumVoltage / 200;
      float error_voltage = 12 / sumVoltage;

      error_voltage = 0.033659 - error_voltage;
      error_voltage = (error_voltage * 1000000);

      if ((error_voltage > 32768)) error_voltage = 32768;
      if (error_voltage < -32768) error_voltage = -32768;

      if (error_voltage < 0) {
        signo = 1;
        error_voltage = error_voltage * (-1);
      }
      unsigned int value = (double)error_voltage;
      byte0 = (byte) (value & 0xFF);
      byte1 = (byte) ((value >> 8) & 0xFF);

      if (signo)bitSet(byte1, 7);

      EEPROM.update(0, byte0);
      EEPROM.update(1, byte1);

      display.clearDisplay();
      display.setCursor(0, 0);
      display.println(F("OK"));
      display.setCursor(0, 20);
      display.println(sumVoltage);
      display.setTextSize(1);

      display.setCursor(0, 40);
      display.println(byte0);
      display.setCursor(0, 50);
      display.println(byte1);

      display.display();
      delay(1000);

    } else {//Adjust current

      float sumCurrent = 0;
      byte byte0 = 0;
      byte byte1 = 0;
      byte signo = 0;

      for (unsigned int i = 0; i < 20000; i++) {
        u = analogRead(current_Pin);
        y = 0.9391 * y + 0.0609 * u;
      }
      sumCurrent = y;//sumVoltage = sumVoltage / 200;
      float error_current =  10 / (973.256 - sumCurrent);
      float error_current_sum = error_current * sumCurrent;

      error_current =  0.023118 - error_current;          // error para ajustar coeficiente de ecuacion
      error_current_sum = 12.5 - error_current_sum;       // error para ajustar suma de ecuacion


      // ajustar coeficiente //
      error_current = (error_current * 1000000);

      if ((error_current > 32768)) error_current = 32768;
      if (error_current < -32768) error_current = -32768;

      if (error_current < 0) {
        signo = 1;
        error_current = error_current * (-1);
      }
      unsigned int value = (double)error_current;
      byte0 = (byte) (value & 0xFF);
      byte1 = (byte) ((value >> 8) & 0xFF);
      if (signo)bitSet(byte1, 7);
      EEPROM.update(2, byte0);
      EEPROM.update(3, byte1);

      // ajusta suma//
      error_current_sum = (error_current_sum * 1000);

      if ((error_current_sum > 32768)) error_current_sum = 32768;
      if (error_current_sum < -32768) error_current_sum = -32768;

      if (error_current_sum < 0) {
        signo = 1;
        error_current_sum = error_current_sum * (-1);
      }
      value = (double)error_current_sum;
      byte0 = (byte) (value & 0xFF);
      byte1 = (byte) ((value >> 8) & 0xFF);
      if (signo)bitSet(byte1, 7);
      EEPROM.update(4, byte0);
      EEPROM.update(5, byte1);


      display.clearDisplay();
      display.setCursor(0, 0);
      display.println(F("A.OK"));
      display.setCursor(0, 20);
      display.setTextSize(1);
      display.println(error_current);
      display.setCursor(0, 30);
      display.println(error_current_sum);


      display.setCursor(0, 40);
      display.println(sumCurrent);
      //display.setCursor(0, 50);
      //display.println(byte1);


      display.display();

      delay(1000);


    }
  }
}

void logicMeasure() {
  bitClear(PORTD, PD3);   //quitar pull up
  bitClear(DDRD, DDD3);   //poner como entrada
  nope(); nope(); nope();

  if (bitRead(PIND, 3))  {// si pin es positivo
    bitSet(PORTB, PB5);//digitalWrite(13, HIGH);
    bitClear(PORTD, PD5);//digitalWrite(5, LOW);

  } else  {
    bitSet(DDRD, DDD3);   //pasos necesarios para poner pin en pull up
    bitSet(PORTD, PD3);
    bitSet(PORTD, PD3);   //poner pullup
    bitClear(DDRD, DDD3);// como entrada
    nope(); nope(); nope(); nope(); nope(); nope(); nope(); nope(); nope(); nope(); nope(); nope(); nope(); nope(); nope(); nope();

    if (bitRead(PIND, 3)) { // si pin es alta impedancia
      bitClear(PORTB, PB5);//digitalWrite(13, LOW);
      bitClear(PORTD, PD5);//digitalWrite(5, LOW);
      Timer1.stop();
      if (timerCounter100mS == 50) {
        digitalWrite(buzzer_Pin, LOW);//////////////**********cambiar esta funcion por la rapida///////
        timerCounter100mS = 0;
      }

    } else { // sin pin es gnd
      bitClear(PORTB, PB5);//digitalWrite(13, LOW);
      bitSet(PORTD, PD5);//digitalWrite(5, HIGH);
      if ((scopeP % 2) == 0) {
        digitalWrite(buzzer_Pin, HIGH);
        timerCounter100mS = 50;
      }
    }
  }
  bitClear(PORTD, PD3); //quitar pullup
  bitSet(DDRD, DDD3);   //poner como salida
}
void pushButtonFunction() {
  static byte byte_corrimiento_Select = 0;
  static byte byte_corrimiento_Mode = 0;
  static byte byte_corrimiento_plus = 0;

  bitWrite(byte_corrimiento_Select, 0, bitRead(PIND, 6));
  byte_corrimiento_Select =  byte_corrimiento_Select << 1;

  bitWrite(byte_corrimiento_Mode, 0, bitRead(PINB, 0));
  byte_corrimiento_Mode =  byte_corrimiento_Mode << 1;

  bitWrite(byte_corrimiento_plus, 0, bitRead(PIND, 7));
  byte_corrimiento_plus =  byte_corrimiento_plus << 1;

  if (byte_corrimiento_Select == 240 ) { // select
    switchPushed = true;           // Switch pressed! Flag ON (used for interrupting waveform saving)
    scopeP++;                      // shift the position to the scope
    digitalWrite(buzzer_Pin, HIGH);
    if (Function == 2)if (scopeP > 2) scopeP = 0;                // return to the first position
      else if (scopeP > 3) scopeP = 0;
    timerCounter100mS = 1;
    timerCounter1sec = 0;

  } else {
    if (byte_corrimiento_Select == 0) timerCounter1sec++;
    if (timerCounter1sec == 2000) {
      scopeP--;
      if (Rotation == 0)Rotation = 2;
      else Rotation = 0;
      timerCounter1sec = 0;
      timerCounter100mS = 1;
      digitalWrite(buzzer_Pin, HIGH);
    }
  }

  if (byte_corrimiento_Mode == 240 ) { // mode
    switchPushed = true;           // Switch pressed! Flag ON (used for interrupting waveform saving)
    hold = ! hold;                 // flag inversion
    digitalWrite(buzzer_Pin, HIGH);
    timerCounter100mS = 1;
    timerCounter1sec = 0;
  } else {
    if (byte_corrimiento_Mode == 0) timerCounter1sec++;
    if (timerCounter1sec == 2000) {
      Function++;
      timerCounter1sec = 0;
    }
  }

  if (byte_corrimiento_plus == 240 ) { // plus
    switchPushed = true;           // Switch pressed! Flag ON (used for interrupting waveform saving)
    timerCounter1sec = 0;
    timerCounter100mS = 1;
    digitalWrite(buzzer_Pin, HIGH);
    if (scopeP == 0) {             // If the scope hits the vertical range
      vRange++;                    // Range UP
      if (vRange > 3) vRange = 0;             // If it is the upper limit
    }
    if (scopeP == 1) {             // If the scope hits the horizontal range
      hRange++;
      if (hRange > 9) hRange = 0;            // If it is the upper limit
    }
    if (scopeP == 2) {             // If the scope is on measure info
      measureRange++;
      if (measureRange > 1) measureRange = 0;            // If it is the upper limit
    }
  } else {
    if (byte_corrimiento_plus == 0) timerCounter1sec++;
    if (timerCounter1sec == 2000) {
      if (scopeP == 0) {             // If the scope hits the vertical range
        if (vRange == 0) vRange = 3;             // If it is the upper limit
        else vRange--;                    // Range UP
      }
      if (scopeP == 1) {             // If the scope hits the horizontal range
        if (hRange == 0) hRange = 9;             // If it is the upper limit
        else hRange--;                    // Range UP
      }
      if (scopeP == 2) {             // If the scope is on measure info
        if (measureRange == 0) measureRange = 1;             // If it is the upper limit
        else measureRange--;                    // Range UP
      }
      digitalWrite(powerLed_Pin, !digitalRead(powerLed_Pin));
      timerCounter1sec = 0;
      timerCounter100mS = 1;
      digitalWrite(buzzer_Pin, HIGH);
    }
  }

  if ((byte_corrimiento_Select == 255) && (byte_corrimiento_Mode == 255) && (byte_corrimiento_plus == 255)) {
    timerCounter1sec = 0;
  }

}
void Voltimiter_logicProbe() {

  display.clearDisplay();                 // Clear all screen (0.4ms)
  float sumVolt = 0;
  float sumCurr = 0;
  int overCurrentTimeProtection = 0;

  u = analogRead(scope_Pin);
  z = analogRead(current_Pin);

  y = 0.7304 * y + 0.2696 * u;//0.7304 * y + 0.2696 * u; fc=0.5;Hd=c2d(H,0.1)
  sumVolt = y;

  x = 0.8987 * x + 0.1013 * z; //0.7304 * x + 0.2696 * z;//0.7304 * y + 0.2696 * u; fc=0.5;Hd=c2d(H,0.1)
  sumCurr = x;



  float Voltage = sumVolt * voltage_Adjust_COEF;//0.03366;//voltage  = (sum/930)*1033/33
  float Current = current_Adjust_COEF * sumCurr;//*sumCurr //(300 * sumCurr) / 4304;
  Current = Current - current_Adjust_SUM; //- 37.5;
  if ((Current < 0.1499) && (Current > (-0.1499))) Current = 0;//Current * (-1);

  if ((scopeP % 2) == 0) {                  //firat check if sound is needed
    //display.setCursor(80, 50);              //display for sund character
    //display.setTextSize(1);
    //display.println(F("bip"));
    display.drawBitmap(80, 40, speak24, 24, 24, 1);//display.drawBitmap(51, 40, speak24, 24, 24, 1);
    if ((Voltage >= 2.5) && ((bitRead(PINB, 5)) || (bitRead(PORTD, 5)))) { //for voltage up to 2.5V else restar the previos
      if (previousV != (byte)Voltage) {     //chec if is the voltage change if change for up or down update timer else previos stays the same as before
        previousV = (byte)Voltage;
        long period = 500000 / previousV;
        if (period < 20000) period = 20000; //restringir a no mayor de 25Hz
        Timer1.restart();
        Timer1.setPeriod(period);
      }
    } else {
      previousV = 0;
      Timer1.stop();
      timerCounter100mS = 50;
    }
  } else {
    Timer1.stop();
    display.drawBitmap(80, 40, noSpeak24, 24, 24, 1);

    digitalWrite(buzzer_Pin, LOW);
    previousV = 0;
  }


  if (Current > 10 || Current < -10 ) {//  si se pasa de corriente

    
  } else {

  }


  
  display.setCursor(3, 0);                  // for voltage display
  display.setTextSize(2);                  // double the character,
  dtostrf(Voltage, 4, 1, chrBuff);        // 1 decimal place (xx.x format)
  display.print(chrBuff);
  display.println(F("V"));
  display.setCursor(3, 30);                  // for voltage display
  dtostrf(Current, 4, 1, chrBuff);        // 1 decimal place (xx.x format)
  display.print(chrBuff);
  display.println(F("A"));

  display.setCursor(15, 16);
  display.setTextSize(1);
  display.print(sumVolt);
  display.setCursor(15, 52);
  display.print(sumCurr);
//
  display.setRotation(Rotation);
  display.display();

}
////*****************************************Scope functions**********************************////

void scope(void) {
  display.setTextSize(1);
  pinMode(logicProbe_Pin, INPUT);
  attachInterrupt(digitalPinToInterrupt(logicProbe_Pin), counting, FALLING ); //for frecuency measurments
  Timer1.stop();
  Timer1.initialize(1000000);
  Timer1.setPeriod(1000000);
  Timer1.attachInterrupt(SaveCount);
  digitalWrite(buzzer_Pin, LOW);
  digitalWrite(blueLed_Pin, LOW);
  digitalWrite(redLed_Pin, LOW);
//  digitalWrite(vibrator_Pin, LOW);
//  digitalWrite(mosfetEnable_Pin, HIGH);             //apagar mosfet

  hold = false;
  vRange = 2;
  hRange = 1;
  scopeP = 1;

  while (Function == 2) {
    setConditions();                          // Set measurement conditions
//    digitalWrite(debug_Pin, HIGH);
    readWave();                                // Waveform reading (1.6-400ms)
//    digitalWrite(debug_Pin, LOW);                    //
    setConditions();                           // Reset measurement conditions (reflect changes in measurement on display)
    dataAnalize();                             // Collect various data information (0.5-0.6ms)
    writeCommonImage();                       // Drawing a fixed image (2.6ms)
    plotData();                               // Waveform plot (10-18ms)
    dispInf();                                // Display various information (6.5-8.5ms)
    display.setRotation(Rotation);
    display.display();                        // transfer buffer and display on OLED (37ms)
    while ((hold == true) && (Function == 2)) {                  // Wait if the Hold button is pressed
      dispHold();
      delay(10);
    }

  }
  detachInterrupt(digitalPinToInterrupt(logicProbe_Pin));
  Timer1.stop();
  Timer1.detachInterrupt();

  Timer1.initialize(1000000);
  Timer1.attachInterrupt(BuzzerToggle);
  digitalWrite(buzzer_Pin, LOW);
  Timer1.stop();

  pinMode(logicProbe_Pin, OUTPUT);
  digitalWrite(logicProbe_Pin, LOW);

  pinMode(resistor_3p3K_pin, OUTPUT);      // Set the attenuator control pin to output
  digitalWrite(resistor_3p3K_pin, LOW);    // outputs LOW
  pinMode(resistor_6p8K_pin, INPUT);       // Set the attenuator control pin to Hi-z (set to input)

  pinMode(logicProbe_Pin, INPUT);             //for frecuency mesurment and logic probe
}

void setConditions() {          // Set measurement conditions
  // Bring range name from PROGMEM
  strcpy_P(hScale, (char*)pgm_read_word(&(hstring_table[hRange])));  // Set horizontal axis range name
  strcpy_P(vScale, (char*)pgm_read_word(&(vstring_table[vRange])));  // Set vertical axis range name

  switch (vRange) {              // Setting for each vertical axis range Voltage
    case 0: {                    // 5V range
        rangeMax = 5 / 0.016888;    /// Set the number of full scale pixels
        rangeMaxDisp = 500;
        rangeMin = 0;
        rangeMinDisp = 0;
        att10x = 1;              // do not use input attenuator
        break;
      }
    case 1: {                   // 10V range
        rangeMax = 10 / 0.016888;  // Set the number of full scale pixels
        rangeMaxDisp = 1000;
        rangeMin = 0;
        rangeMinDisp = 0;
        att10x = 1;               // Use input attenuator
        break;
      }
    case 2: {                  // 20V range
        rangeMax = 20 / voltage_Adjust_COEF; // Set the number of full scale pixels
        rangeMaxDisp = 2000;
        rangeMin = 0;
        rangeMinDisp = 0;
        att10x = 0;              // Use input attenuator
        break;
      }
    case 3: {    // 50V range
        rangeMax = 50 / voltage_Adjust_COEF; // Set the number of full scale pixels
        rangeMaxDisp = 5000;     // Vertical scale. Set by 100 times value
        rangeMin = 0;
        rangeMinDisp = 0;
        att10x = 0;              // Use input attenuator
        break;
      }
  }
}

void readWave() {                            // Record waveform in memory
  int timeExec = 0;                            // Approximate execution time of current range (ms)
  if (att10x == 1) {                         // If 6.8k attenuator is needed
    pinMode(resistor_6p8K_pin, OUTPUT);      // Set the attenuator control pin to output
    digitalWrite(resistor_6p8K_pin, LOW);    // outputs LOW
    pinMode(resistor_3p3K_pin, INPUT);       // Set the attenuator control pin to Hi-z (set to input)
  } else {                                   // If 3.3k attenuator is needed
    pinMode(resistor_3p3K_pin, OUTPUT);      // Set the attenuator control pin to output
    digitalWrite(resistor_3p3K_pin, LOW);    // outputs LOW
    pinMode(resistor_6p8K_pin, INPUT);       // Set the attenuator control pin to Hi-z (set to input)
  }
  switchPushed = false;                      // Clear switch operation flag
  switch (hRange) {                          // Change recording speed according to range
    case 0: {                               // 200ms range
        timeExec = 1600 + 60;                // Approximate execution time (ms) Used for countdown to EEPROM storage
        ADCSRA = ADCSRA & 0xf8;               // Clear the lower 3 bits
        ADCSRA = ADCSRA | 0x07;              // Divide ratio 128 (arduino original)
        for (int i = 0; i < REC_LENGTH; i++) {  // REC_LENGTH count
          waveBuff[i] = analogRead(scope_Pin);  // about 112μs
          delayMicroseconds(7888);           // Sampling period adjustment
          if (switchPushed == true) {        // If there is a switch operation
            switchPushed = false;
            break;                           // Stop recording (improve response by exiting the loop)
          }
        }
        break;
      }
    case 1: {                                // 100ms range
        timeExec = 800 + 60;                 // Approximate execution time (ms) Used for countdown to EEPROM storage
        ADCSRA = ADCSRA & 0xf8;              // Clear the lower 3 bits
        ADCSRA = ADCSRA | 0x07;              // Divide ratio 128 (arduino original)
        for (int i = 0; i < REC_LENGTH; i++) {  // REC_LENGTH count
          waveBuff[i] = analogRead(scope_Pin); // about 112μs
          delayMicroseconds(3888);           // Sampling cycle adjustment
          if (switchPushed == true) {        // If there is a switch operation
            switchPushed = false;
            break;                            // Stop recording (improve response by exiting the loop)
          }
        }
        break;
      }
    case 2: {                                // 50ms range
        timeExec = 400 + 60;                // Approximate execution time (ms) Used for countdown to EEPROM storage
        ADCSRA = ADCSRA & 0xf8;              // Clear the lower 3 bits
        ADCSRA = ADCSRA | 0x07;              // Divide ratio 128 (arduino original)
        for (int i = 0; i < REC_LENGTH; i++) {  // REC_LENGTH count
          waveBuff[i] = analogRead(scope_Pin);// about 112μs
          delayMicroseconds(1888);           // Sampling cycle adjustment
          if (switchPushed == true) {        // If there is a switch operation
            break;                           // Stop recording (improve response by exiting the loop)
          }
        }
        break;
      }
    case 3: {                                // 20ms range
        timeExec = 160 + 60;                 // Approximate execution time (ms) Used for countdown to EEPROM storage
        ADCSRA = ADCSRA & 0xf8;              // Clear the lower 3 bits
        ADCSRA = ADCSRA | 0x07;             // Divide ratio 128 (arduino original)
        for (int i = 0; i < REC_LENGTH; i++) {  // REC_LENGTH count
          waveBuff[i] = analogRead(scope_Pin);// about 112μs
          delayMicroseconds(688);            // Adjust sampling period
          if (switchPushed == true) {        // If there is a switch operation
            break;                           // Stop recording (improve response by exiting the loop)
          }
        }
        break;
      }
    case 4: {                                // 10 ms range
        timeExec = 80 + 60;                   // Approximate execution time (ms) Used for countdown to EEPROM storage
        ADCSRA = ADCSRA & 0xf8;              // Clear the lower 3 bits
        ADCSRA = ADCSRA | 0x07;              // Divide ratio 128 (arduino original)
        for (int i = 0; i < REC_LENGTH; i++) {  // REC_LENGTH count
          waveBuff[i] = analogRead(scope_Pin);// about 112μs
          delayMicroseconds(288);            // Sampling period adjustment
          if (switchPushed == true) {        // If there is a switch operation
            break;                           // Stop recording (improve response by exiting the loop)
          }
        }
        break;
      }
    case 5: {                               // 5 ms range
        timeExec = 40 + 60;                  // Approximate execution time (ms) Used for countdown to EEPROM storage
        ADCSRA = ADCSRA & 0xf8;              // Clear the lower 3 bits
        ADCSRA = ADCSRA | 0x07;              // Divide ratio 128 (arduino original)
        for (int i = 0; i < REC_LENGTH; i++) {  // REC_LENGTH count
          waveBuff[i] = analogRead(scope_Pin);// about 112μs
          delayMicroseconds(88);             // Adjust sampling period
          if (switchPushed == true) {        // If there is a switch operation
            break;                           // Stop recording (improve response by exiting the loop)
          }
        }
        break;
      }
    case 6: {                                // 2 ms range
        timeExec = 16 + 60;                  // Approximate execution time (ms) Used for countdown to EEPROM storage
        ADCSRA = ADCSRA & 0xf8;             // Clear the lower 3 bits
        ADCSRA = ADCSRA | 0x06;              // Divide ratio 64 (0x1 = 2, 0x2 = 4, 0x3 = 8, 0x4 = 16, 0x5 = 32, 0x6 = 64, 0x7 = 128)
        for (int i = 0; i < REC_LENGTH; i++) {  // REC_LENGTH count
          waveBuff[i] = analogRead(scope_Pin); // about 56μs
          delayMicroseconds(24);             // Adjust sampling period
        }
        break;
      }
    case 7: {                                // 1 ms range
        timeExec = 8 + 60;                   // Approximate execution time (ms) Used for countdown to EEPROM storage
        ADCSRA = ADCSRA & 0xf8;              // Clear the lower 3 bits
        ADCSRA = ADCSRA | 0x05;             // Division ratio 16 (0x1 = 2, 0x2 = 4, 0x3 = 8, 0x4 = 16, 0x5 = 32, 0x6 = 64, 0x7 = 128)
        for (int i = 0; i < REC_LENGTH; i++) {  // REC_LENGTH count
          waveBuff[i] = analogRead(scope_Pin);// about 28μs
          delayMicroseconds(12);             // Sampling period adjustment
        }
        break;
      }
    case 8: {                                // 500us range
        timeExec = 4 + 60;                   // Approximate execution time (ms) Used for countdown to EEPROM storage
        ADCSRA = ADCSRA & 0xf8;              // Clear the lower 3 bits
        ADCSRA = ADCSRA | 0x04;              // Division ratio 16 (0x1 = 2, 0x2 = 4, 0x3 = 8, 0x4 = 16, 0x5 = 32, 0x6 = 64, 0x7 = 128)
        for (int i = 0; i < REC_LENGTH; i++) {  // REC_LENGTH count
          waveBuff[i] = analogRead(scope_Pin); // about 16μs
          delayMicroseconds(4);              // Adjust sampling period
          // Time fine adjustment 1.875μs (1 clock per nop, 0.0625μs @ 16MHz)
          asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop");
          asm("nop"); asm("nop"); asm("nop");
        }
        break;
      }
    case 9: {                                // 200us range
        timeExec = 2 + 60;                   // Approximate execution time (ms) Used for countdown to EEPROM storage
        ADCSRA = ADCSRA & 0xf8;              // Clear the lower 3 bits
        ADCSRA = ADCSRA | 0x02;              // Division ratio: 4 (0x1 = 2, 0x2 = 4, 0x3 = 8, 0x4 = 16, 0x5 = 32, 0x6 = 64, 0x7 = 128)
        for (int i = 0; i < REC_LENGTH; i++) {  // REC_LENGTH count
          waveBuff[i] = analogRead(0);       // about 6μs
          // Time fine adjustment 1.875μs (1 clock per nop, 0.0625μs @ 16MHz)
          asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop");
          asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop");
          asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop");
        }
        break;
      }
  }
}

void dataAnalize() {                      // Set various information for drawing
  int d;
  long sum = 0;

  // Find the maximum and minimum values
  dataMin = 1023;                         // minimum
  dataMax = 0;                           // initialize the maximum value recording variable
  for (int i = 0; i < REC_LENGTH; i++) {// Find maximum and minimum values
    d = waveBuff[i];
    sum = sum + d;
    if (d < dataMin) {                    // min and
      dataMin = d;
    }
    if (d > dataMax) {                    // find the maximum
      dataMax = d;
    }
  }

  // find the average
  dataAve = (sum + 10) / 20;              // Average value calculation (calculated with 10 times value to ensure accuracy)
  // Find the trigger position
  for (trigP = ((REC_LENGTH / 2) - 51); trigP < ((REC_LENGTH / 2) + 50); trigP++) {// Find the point across the median in the middle of the data range
    if ((waveBuff[trigP - 1] < (dataMax + dataMin) / 2) && (waveBuff[trigP] >= (dataMax + dataMin) / 2)) {
      break;                             // Rising trigger detected!
    }
  }
  trigSync = true;
  if (trigP >= ((REC_LENGTH / 2) + 50)) {  // If no trigger is found in the range
    trigP = (REC_LENGTH / 2);              // For now, set it to the center,
    trigSync = false;                      // Unsync display flag
  }
  if ((dataMax - dataMin) <= MIN_TRIG_SWING) {// If the waveform amplitude is smaller than the specified value
    trigSync = false;                      // Unsync display flag
  }
}

void writeCommonImage() {                   // draw common screen as lines
  display.clearDisplay();                   // Clear all screen (0.4ms)
  display.setTextColor(WHITE);              // draw with white text

  display.drawFastVLine(0, 9, 55, WHITE);  // left vertical line
  display.drawFastVLine(127, 9, 55, WHITE); // left vertical line

  display.drawFastHLine(0, 9, 5, WHITE);   // Max value auxiliary mark
  display.drawFastHLine(0, 36, 2, WHITE);  //
  display.drawFastHLine(0, 63, 5, WHITE);  //

  display.drawFastHLine(30, 9, 3, WHITE);   // Max value auxiliary mark
  display.drawFastHLine(30, 63, 3, WHITE);  //

  display.drawFastHLine(62, 9, 3, WHITE);   // Max value auxiliary mark
  display.drawFastHLine(62, 63, 3, WHITE);  //

  display.drawFastHLine(94, 9, 3, WHITE);  // Max value auxiliary mark
  display.drawFastHLine(94, 63, 3, WHITE); //

  display.drawFastHLine(123, 9, 5, WHITE);  // Right end Max value auxiliary mark
  display.drawFastHLine(123, 63, 5, WHITE); // 　

  for (int x = 0; x <= 128; x += 5) {
    display.drawFastHLine(x, 36, 2, WHITE); // Draw center line (horizontal line) with dotted line
  }
  for (int x = (127 - 32); x > 30; x -= 32) {
    for (int y = 10; y < 63; y += 5) {
      display.drawFastVLine(x, y, 2, WHITE); // Draw three vertical lines with dotted lines
    }
  }
}

void plotData() {                    // plot data based on array values
  int overSampling = 0;
  long y1, y2;
  for (int x = 0; x <= 98; x++) {
    y1 = map(waveBuff[x + trigP - 50], rangeMin, rangeMax, 63, 9); // Convert to plot coordinates  map(value, fromLow, fromHigh, toLow, toHigh)
    y1 = constrain(y1, 9, 63);                                     // Crush the protruding part
    y2 = map(waveBuff[x + trigP - 49], rangeMin, rangeMax, 63, 9); //
    y2 = constrain(y2, 9, 63);                                     //
    //display.drawLine(x + 27, y1, x + 28, y2, WHITE);               // Connect the points with a line
    if ((x % 4) == 0) {
      display.drawLine(x + 1 + overSampling, y1, x + 3 + overSampling, y2, WHITE);               // Connect the points with a line
      overSampling++;
    } else {
      display.drawLine(x + 1 + overSampling, y1, x + 2 + overSampling, y2, WHITE);         // Connect the points with a line
    }
  }
}

void dispInf() {                            // Various information display
  float voltage;

  //Frecuency display
  if (counterBackup < 9 && counterBackup >= 0) {
    display.setCursor(109, 0);                   // one digit frecuency
    display.println(counterBackup);
    display.setCursor(115, 0);
    display.println(F("Hz"));
  }
  if (counterBackup < 99 && counterBackup >= 10) {
    display.setCursor(103, 0);                   // two digit frecuency
    display.println(counterBackup);
    display.setCursor(115, 0);
    display.println(F("Hz"));
  }
  if (counterBackup < 999 && counterBackup >= 100) {
    display.setCursor(97, 0);                   // three digit frecuency
    display.println(counterBackup);
    display.setCursor(115, 0);
    display.println(F("Hz"));
  }
  if (counterBackup < 9999 && counterBackup >= 1000) {
    int rounding = counterBackup / 1000;
    display.setCursor(103, 0);                   // 1 digit frecuency x 1000
    display.println(rounding);
    display.setCursor(109, 0);
    display.println(F("KHz"));
  }
  if (counterBackup < 99999 && counterBackup >= 10000) {
    int rounding = counterBackup / 1000;
    display.setCursor(97, 0);                   // 1 digit frecuency x 1000
    display.println(rounding);
    display.setCursor(109, 0);
    display.println(F("KHz"));
  }

  if (scopeP == 0) {                         // If the scope is hit
    display.setTextColor(BLACK, WHITE); // Draw 'inverse' text
    display.setCursor(2, 0);                  //
    display.print(vScale);
    display.setTextColor(WHITE);
    display.setCursor(21, 0);
    display.print(hScale);                     // Horizontal scale (time / div) display
  }

  if (scopeP == 1) {                         // If the scope is hit
    display.setCursor(2, 0);                  //
    display.print(vScale);
    display.setTextColor(BLACK, WHITE);
    display.setCursor(21, 0);
    display.print(hScale);                     // Horizontal scale (time / div) display
    display.setTextColor(WHITE);
  }

  //Measue display
  if (scopeP == 2) {      // If the scope is hit
    display.setTextColor(WHITE);
    display.setCursor(2, 0);                  //
    display.print(vScale);

    display.setCursor(21, 0);
    display.print(hScale);                     // Horizontal scale (time / div) display
    display.setTextColor(BLACK, WHITE);
  }
  ///////////////////////////////////// Average voltage display////////////////////
  if (measureRange == 0) {                    //display average

    if (att10x == 0) {                         // If there is a 10x attenuator
      voltage = dataAve * voltage_Adjust_COEF / 10.0;       // Voltage calculation with sensitivity coefficient in 50V range
    } else {
      voltage = dataAve * 0.01688 / 10.0;   //lsb5V/ 10.0;   CAMBIAR!!!!!         // Voltage calculation with 5V range sensitivity coefficient
    }
    if (voltage < 10.0) {                     // if the voltage is less than 10V
      dtostrf(voltage, 4, 2, chrBuff);         // 2 digits after the decimal point (x.xx format)
    } else {                                   // 10V or more
      dtostrf(voltage, 4, 1, chrBuff);         // 1 decimal place (xx.x format)
    }
    display.setCursor(53, 0);                  // On the top right of the screen
    display.print(chrBuff);                     // display the average voltage
    display.setCursor(77, 0);
    display.println(F("Vpr"));
  }
  if (measureRange == 1) {                    //display max voltage
    if (att10x == 0) {                         // If there is a 10x attenuator
      voltage = dataMax * voltage_Adjust_COEF;       // Voltage calculation with sensitivity coefficient in 50V range
    } else {
      voltage = dataMax * 0.01688;      //lsb5V;   CAMBIAR!!!!!     // Voltage calculation with 5V range sensitivity coefficient
    }
    if (voltage < 10.0) {                     // if the voltage is less than 10V
      dtostrf(voltage, 4, 2, chrBuff);         // 2 digits after the decimal point (x.xx format)
    } else {                                   // 10V or more
      dtostrf(voltage, 4, 1, chrBuff);         // 1 decimal place (xx.x format)
    }
    display.setCursor(53, 0);                  // On the top right of the screen
    display.print(chrBuff);                     // display the average voltage
    display.setCursor(77, 0);
    display.println(F("Vmx"));

  }
  display.setTextColor(WHITE);
  ///////////////////////////// vertical scale display///////////////////////////////////////
  voltage = rangeMaxDisp / 100.0;           // Convert Max voltage
  if (vRange == 0 ) {           // If sensitivity is 5V
    dtostrf(voltage, 4, 2, chrBuff);        // Convert to *. ** format
  } else {                                   //
    dtostrf(voltage, 4, 1, chrBuff);         // convert to **. * format
  }
  //  display.setCursor(0, 9);
  //  display.print(chrBuff);                    ///////////////// Max value display

  voltage = (rangeMaxDisp + rangeMinDisp) / 200.0; // calculate the median
  if (vRange == 0) {          // If sensitivity is 5V or less or Auto5V
    dtostrf(voltage, 4, 2, chrBuff);        // 2 decimal places
  } else {                                   //
    dtostrf(voltage, 4, 1, chrBuff);        // 1 decimal place
  }
  //  display.setCursor(0, 33);
  //  display.print(chrBuff);                    /////////////////// center value display

  voltage = rangeMinDisp / 100.0;            // calculate Min value
  if (vRange == 0) {           // If sensitivity is 5V or less or Auto5V
    dtostrf(voltage, 4, 2, chrBuff);         // 2 digits below several points
  } else {
    dtostrf(voltage, 4, 1, chrBuff);         // 1 decimal place
  }
  //  display.setCursor(0, 57);
  //  display.print(chrBuff);                    ///////////////////// Min value display

  // Trigger detection error display
  if (trigSync == false) {                   // If trigger detection fails
    display.fillRect(85, 11, 36, 8, BLACK);  // 4 characters black
    display.setCursor(85, 11);               // In the upper right of the graph area
    display.print(F("Sincro"));              // display Unsync
  }
}

void dispHold() {                            // display Hold on screen
  display.fillRect(42, 11, 24, 8, BLACK);    // 4 characters black
  display.setCursor(42, 11);
  display.print(F("Pausa"));                 // Hold display
  display.display();                         //
}

void counting(void) {
  counter++;
}
void SaveCount(void) {
  counterBackup = counter;
  counter = 0;
}

////*********************ISR*************////
void startTimer2() {
  //set timer2 interrupt at 8kHz
  TCCR2A = 0;// set entire TCCR2A register to 0
  TCCR2B = 0;// same for TCCR2B
  TCNT2  = 0;//initialize counter value to 0

  OCR2A = 61;// par
  // turn on CTC mode
  TCCR2A |= (1 << WGM21);
  // Set CS22 bit for 1024 prescaler
  TCCR2B |= (1 << CS22);
  TCCR2B &= ~(1 << CS21);//TCCR2B |= (1 << CS21);
  TCCR2B |= (1 << CS20);//TCCR2B &= ~(1 << CS20);//zero, before 1 -->TCCR2B |= (1 << CS20);
  // enable timer compare interrupt
  TIMSK2 |= (1 << OCIE2A);

  //ts=(2*Preescaler*(OCR2A+1))/CLKmicro)

}
void nope(void) {
  asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop");
  asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop");
  asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop");
  asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop");
  asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop"); asm("nop");
}
ISR(TIMER2_COMPA_vect) {
  //bitSet(PORTB, PB5);

  if ((timerCounter100mS > 0) && (timerCounter100mS < 50)) timerCounter100mS++;
  if (Function == 0)logicMeasure();
  else if ((Function == 2) && (timerCounter100mS == 50)) digitalWrite(buzzer_Pin, LOW); //if  function scope is on and change rotation or power led

  pushButtonFunction();
  //digitalWrite(vibrator_Pin, LOW);
  // bitClear(PORTB, PB5);
}
