using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public struct SaveDatas
{
    public ulong curStage;
    public ulong highstStage;
    public ulong money;
    public ulong dia;
    public ulong soul;
    public ulong goldPowerLv;
    public ulong soulPowerLv;
    public ulong weaponSkillLv;
    public byte isSound;
    public List<int> haveWeapon;
    public List<int> haveShield;
    public List<int> haveHelmet;

    public int[] playerEquip;

    public SaveDatas(int x)
    {
        curStage = 0;
        highstStage = 0;
        money = 0;
        dia = 0;
        soul = 0;
        goldPowerLv = 0;
        soulPowerLv = 0;
        weaponSkillLv = 0;
        isSound = 0;
        haveWeapon = new List<int>();
        haveShield = new List<int>();
        haveHelmet = new List<int>();
        playerEquip = new int[3];
    }
    public static byte[] StructToBytes(object obj)
    {
        //구조체 사이즈 
        int iSize = Marshal.SizeOf(obj);

        //사이즈 만큼 메모리 할당 받기
        byte[] arr = new byte[iSize];

        IntPtr ptr = Marshal.AllocHGlobal(iSize);
        //구조체 주소값 가져오기
        Marshal.StructureToPtr(obj, ptr, false);
        //메모리 복사 
        Marshal.Copy(ptr, arr, 0, iSize);
        Marshal.FreeHGlobal(ptr);

        return arr;
    }
    public static T ByteToStruct<T>(byte[] buffer) where T : struct
    {
        //구조체 사이즈 
        int size = Marshal.SizeOf(typeof(T));
        IntPtr ptr;
        T obj;
        if (size > buffer.Length)
        {
            ptr = Marshal.AllocHGlobal(size);
            for(int i =0; i < size; i++)
            {
                Marshal.WriteByte(ptr+(size-buffer.Length),i,0);
            }
            Marshal.Copy(buffer, 0, ptr, size);
            obj = (T)Marshal.PtrToStructure(ptr, typeof(T));
            Marshal.FreeHGlobal(ptr);
            return obj;
        }

        ptr = Marshal.AllocHGlobal(size);
        Marshal.Copy(buffer, 0, ptr, size);
        obj = (T)Marshal.PtrToStructure(ptr, typeof(T));
        Marshal.FreeHGlobal(ptr);
        return obj;
    }
}
