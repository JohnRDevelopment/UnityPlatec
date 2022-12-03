/******************************************************************************
 *  plate-tectonics, a plate tectonics simulation library
 *  Copyright (C) 2012-2013 Lauri Viitanen
 *  Copyright (C) 2014-2015 Federico Tomassetti, Bret Curtis
 *
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 2.1 of the License, or (at your option) any later version.
 *
 *  This library is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *  Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, see http://www.gnu.org/licenses/
 *****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Assertions;
// #include "geometry.hpp"


public class IntPoint
{
    //
    // IntPoint
    //

    public int x;
    public int y;
    public IntPoint(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    int getX()
    {
        return x;
    }

    int getY()
    {
        return y;
    }
}

public class FloatPoint
{
    //
    // FloatPoint
    //

    public float x;
    public float y;

    public FloatPoint(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public float getX()
    {
        return x;
    }

    public float getY()
    {
        return y;
    }

    void shift(float dx, float dy, WorldDimension worldDimension)
    {
        UInt32 worldwidth = worldDimension.getWidth();
        x += dx;
        x += x > 0 ? 0 : worldwidth;
        x -= x < worldwidth ? 0 : worldwidth;

        UInt32 worldheight = worldDimension.getHeight();
        y += dy;
        y += y > 0 ? 0 : worldheight;
        y -= y < worldheight ? 0 : worldheight;

        Assert.IsTrue(worldDimension.contains(this), "Point not in world!");
    }
}

public class Dimension
{
    //
    // Dimension
    //

    public UInt32 width; 
    public UInt32 height;
    public Dimension(UInt32 width, UInt32 height)
    {
        this.width = width;
        this.height = height;
    }

    public Dimension(Dimension original)
    {
        width = original.getWidth();
        height = original.getHeight();
    }

    public bool contains(UInt32 x, UInt32 y)
    {
        return (x >= 0 && x < width && y >= 0 && y < height);
    }

    public bool contains(float x, float y)
    {
        return (x >= 0 && x < width && y >= 0 && y < height);
    }

    public bool contains(FloatPoint p)
    {
        return (p.getX() >= 0 && p.getX() < width && p.getY() >= 0 && p.getY() < height);
    }

    void grow(UInt32 amountX, UInt32 amountY)
    {
        width += amountX;
        height += amountY;
    }

    public UInt32 getWidth(){
        return width;
    }
    public UInt32 getHeight(){
        return height;
    }
}

public class WorldDimension
{
    //
    // WorldDimension
    //

    public Dimension dimension;
    public UInt32 width;
    public UInt32 height;

    public bool contains(UInt32 x, UInt32 y)
    {
        return (x >= 0 && x < width && y >= 0 && y < height);
    }

    public bool contains(float x, float y)
    {
        return (x >= 0 && x < width && y >= 0 && y < height);
    }

    public bool contains(FloatPoint p)
    {
        return (p.getX() >= 0 && p.getX() < width && p.getY() >= 0 && p.getY() < height);
    }

    public WorldDimension(UInt32 width, UInt32 height)
    {
        dimension = new Dimension(width, height);
    }

    public WorldDimension(Dimension original)
    {
        dimension = new Dimension(original);
    }

    public UInt32 getMax()
    {
        return width > height ? width : height;
    }

    UInt32 xMod(UInt32 x)
    {
        return (x + width) % width;
    }

    UInt32 yMod(UInt32 y)
    {
        return (y + height) % height;
    }

    public UInt32 getWidth(){
        return width;
    }
    public UInt32 getHeight(){
        return height;
    }

    void normalize(UInt32 x, UInt32 y)
    {
        x %= width;
        y %= height;
    }

    UInt32 indexOf(UInt32 x, UInt32 y)
    {
        return y * dimension.getWidth() + x;
    }

    UInt32 lineIndex(UInt32 y)
    {
        Assert.IsTrue(y >= 0 && y < height, "y is not valid");
        return indexOf(0, y);
    }

    UInt32 yFromIndex(UInt32 index)
    {
        return index / width;
    }

    UInt32 xFromIndex(UInt32 index)
    {
        UInt32 y = yFromIndex(index);
        return index - y * width;
    }

    UInt32 normalizedIndexOf(UInt32 x, UInt32 y)
    {
        return indexOf(xMod(x), yMod(y));
    }

    UInt32 xCap(UInt32 x)
    {
        return x < width ? x : (width-1);
    }

    UInt32 yCap(UInt32 y)
    {
        return y < height ? y : (height-1);
    }

    UInt32 largerSize()
    {
        return width > height ? width : height;
    }
}
