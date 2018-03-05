
#include <FileIO.h>
#include <SPI.h>
#include <PubSubClient.h>
#include <BridgeClient.h>
#include <ArduinoJson.h>
//#include <Servo.h> 
//bridge area

IPAddress server(your ip);
String output;
char jsonChar[128];
int len;
int command0;
void callback(char* topic, byte* payload, unsigned int length) ;
char buf[128+1];
char z[1000]; // 12 資料1組 ， 總共1000組
BridgeClient ethClient;
PubSubClient client(server, 1883, callback, ethClient);
StaticJsonBuffer<100> jsonBuffer;
JsonObject& root = jsonBuffer.createObject();
void reconnect() {
 while (!client.connected()) {
    Serial.print("Attempting MQTT connection...");
    // Attempt to connect
    if (client.connect("ArduinoClient")) {
      Serial.println("connected");
      // Once connected, publish an announcement...
      client.publish("H1","start");
      // ... and resubscribe
      client.subscribe("inTopic");
    } else {
      Serial.print("failed, rc=");
      Serial.print(client.state());
      Serial.println(" try again in 5 seconds");
      // Wait 5 seconds before retrying
      delay(5000);
    }
  }
}
void callback(char* topic, byte* payload, unsigned int length) {
    /*for (int i=0;i<length;i++) {
    Serial.print((char)payload[i]);
  }
  Serial.println();*/
   byte* p = (byte*)malloc(length);
    // Copy the payload to the new buffer
  memcpy(p,payload,length);
  char *jsonin= (char*)malloc(length);
   for (int i=0;i<length;i++) {
    jsonin[i] = char(p[i] & 0xff);
}

DynamicJsonBuffer jsonBufferIN;   
JsonObject& rootin = jsonBufferIN.parseObject(jsonin);
if (!rootin.success()) {
    Serial.println("parseObject() failed");
    
  }
}

void setup() {
    Bridge.begin();
    Serial.begin(300);
    BridgeFileSystem.begin(); // 初始化檔案系統
    /*while(!Serial && millis() < 5000){
    }*/
}

void log_read(const char *filepath){
    if(BridgeFileSystem.exists(filepath)){
        BridgeFile file = BridgeFileSystem.open(filepath, FILE_READ);
        if(file){
            // 讀取緩衝區，假定檔案的一行最多128個字元
            int cnt;
            while( (cnt = file.readBytesUntil('\n', buf, 128)) > 0){
                buf[cnt] = '\0'; // +1 是為了放進'\0'
                //Serial.println(buf);
                //client.publish("HA",buf);
                root["data"]= buf;
                output="";
                root.printTo(output); 
               // Serial.println(buf.length());
               Serial.println(output);
               len = output.length();
               output.toCharArray(jsonChar, len+1);
               client.publish("H1",jsonChar, len+1);
              // Serial.println(ethClient);
               
            }
            file.close();
        }
        else{
            Serial.print("File open failed: ");
            Serial.println(filepath);
        }
    }
    else{
        Serial.print("File doesn't exist: ");
        Serial.println(filepath);
    }
}

// 檔案路徑
const char filepath[] = "/mnt/sda1/5data/anterior_patient081_s0346lre.csv";
// 若想儲存在micro SD卡裡，路徑需改成/mnt/sda1/
// const char filepath[] = "/mnt/sda1/data.txt";

void loop () 
{
   if (!client.connected()) {
    reconnect();
  }
  client.loop();
  
    if(Serial.available()){
        char c = Serial.read();
        switch(c){
            case 'r':{
                log_read(filepath);
                if(c = 's'){
                 break;
                }
            }
           
          case 's':{
             break;
            }
            
        }
    }
}
