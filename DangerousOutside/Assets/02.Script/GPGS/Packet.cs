using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PacketResolver
{
    public byte[] buffer { get; set; }
    public int position { get; private set; }
    public int size { get; private set; }
    public PacketResolver(int size)
    {
        this.buffer = new byte[size];
        this.position = 0;
    }
    public PacketResolver(byte[] source)
    {
        this.buffer = source;
        this.size = source.Length;
        this.position = 0;
    }
    public void overwrite(byte[] source, int position)
    {
        this.buffer = new byte[source.Length];
        Array.Copy(source, this.buffer, source.Length);
        this.position = position;
    }

    public byte pop_byte()
    {
        try
        {
            byte data = this.buffer[this.position];
            this.position += sizeof(byte);
            return data;
        }
        catch (Exception e)
        {
            return 0;
        }
    }

    public Int16 pop_int16()
    {
        try
        {
            Int16 data = BitConverter.ToInt16(this.buffer, this.position);
            this.position += sizeof(Int16);
            return data;
        }
        catch (Exception e)
        {
            return 0;
        }
    }

    public Int32 pop_int32()
    {
        try
        {
            Int32 data = BitConverter.ToInt32(this.buffer, this.position);
            this.position += sizeof(Int32);
            return data;
        }
        catch (Exception e)
        {
            return 0;
        }
    }

    public Int64 pop_int64()
    {
        try
        {
            Int64 data = BitConverter.ToInt64(this.buffer, this.position);
            this.position += sizeof(Int64);
            return data;
        }
        catch (Exception e)
        {
            return 0;
        }
    }

    public string pop_string()
    {
        try
        {
            Int16 len = BitConverter.ToInt16(this.buffer, this.position);
            this.position += sizeof(Int16);
            string data = System.Text.Encoding.UTF8.GetString(this.buffer, this.position, len);
            this.position += len;

            return data;
        }
        catch (Exception e)
        {
            return "";
        }
    }

    public float pop_float()
    {
        try
        {
            float data = BitConverter.ToSingle(this.buffer, this.position);
            this.position += sizeof(float);
            return data;
        }
        catch (Exception e)
        {
            return 0;
        }
    }
    public void push_int16(Int16 data)
    {
        byte[] temp_buffer = BitConverter.GetBytes(data);
        temp_buffer.CopyTo(this.buffer, this.position);
        this.position += temp_buffer.Length;
    }

    public void push(byte data)
    {
        byte[] temp_buffer = BitConverter.GetBytes(data);
        temp_buffer.CopyTo(this.buffer, this.position);
        this.position += sizeof(byte);
    }

    public void push(Int16 data)
    {
        byte[] temp_buffer = BitConverter.GetBytes(data);
        temp_buffer.CopyTo(this.buffer, this.position);
        this.position += temp_buffer.Length;
    }

    public void push(Int32 data)
    {
        byte[] temp_buffer = BitConverter.GetBytes(data);
        temp_buffer.CopyTo(this.buffer, this.position);
        this.position += temp_buffer.Length;
    }

    public void push(string data)
    {
        byte[] temp_buffer = Encoding.UTF8.GetBytes(data);

        Int16 len = (Int16)temp_buffer.Length;
        byte[] len_buffer = BitConverter.GetBytes(len);
        len_buffer.CopyTo(this.buffer, this.position);
        this.position += sizeof(Int16);

        temp_buffer.CopyTo(this.buffer, this.position);
        this.position += temp_buffer.Length;
    }

    public void push(float data)
    {
        byte[] temp_buffer = BitConverter.GetBytes(data);
        temp_buffer.CopyTo(this.buffer, this.position);
        this.position += temp_buffer.Length;
    }

    public void push(byte[] temp_buffer)
    {
        temp_buffer.CopyTo(this.buffer, this.position);
        this.position += temp_buffer.Length;
    }
}
