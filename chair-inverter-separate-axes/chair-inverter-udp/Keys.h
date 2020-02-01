#pragma once

#include <windows.h>

class Key
{
public:
	bool pressed;
	bool oldPressed;
	bool justPressed;
	char key;

	Key()
	{

	}

	Key(char key)
	{
		this->key = key;
	}

	void update(void)
	{
		pressed = !!(GetAsyncKeyState(this->key) & 0x8000);
		justPressed = (!oldPressed && pressed);
		oldPressed = pressed;
	}
};

class ShortcutKeys
{
public:
	Key invertRollKey;
	Key invertSurgeKey;

	ShortcutKeys(void)
	{
		invertSurgeKey = Key(VK_F1);
		invertRollKey = Key(VK_F2);
	}

	void update(void)
	{
		invertRollKey.update();
		invertSurgeKey.update();
	}
};