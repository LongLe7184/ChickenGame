# ChickenGame
_This indie game was created for learning purpose!_

+ Subject    : SoC Lab - HCMUS
+ Topic      : Development an application for G-Sensor on DE10 board.
+ Description: Using G-Sensor on the DE10 to control a jetplane (player) dodges attacking chickens.

![image](https://github.com/user-attachments/assets/dac34634-3a7a-4123-97df-bacb8d38d6ab)

**_The lastest version is 1.1 - Please check the branchs list_**

**Version 1.0**
_The game is still in progess_
- Chickens is now spawn slowly, sequentially. It can dynamically changes based on the difficulty level system.
- "Game Over " notification message box will pop up whenever chickens collide with player. "Try Again" button will also appears to let the player restart from the beginning.
- Scoreboard was added to record player's achivement.

NOTE: Currently, the player is not controllable yet. This feature will be added when there is a peripheral device send signal to the app.
(I will try to use an Arduino adapt with a joy stick to test this future)

**Data Transmittion Frame**
Values getting from the controller will be processing, and generate to 3 main parameter:
- x Value: control player horizontally
- y Value: control player vertically
- button Value (optional): this physical button replacing "Try Again" on-screen button
These parameter will be send through Serial Port under **String format** and processing as below figure:
![image](https://github.com/user-attachments/assets/c3568ece-4b3b-453a-a1e9-d73fbfd234a6)

**Additional Reference**
_Testing Setup_
![image](https://github.com/user-attachments/assets/28acb836-20f5-4da9-9464-9878bc575df3)

_Joystick Dealing_
Here is how I config the joystick and dealing with the analog data input.
![image](https://github.com/user-attachments/assets/016bf85c-9af3-4827-af16-49f4f21705e2)
