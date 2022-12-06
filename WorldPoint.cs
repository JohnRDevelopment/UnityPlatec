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
using UnityEngine.Assertions;
using System;

// #include "world_point.hpp"
// #include "rectangle.hpp"
// #ifndef WORLD_POINT_HPP
// #define WORLD_POINT_HPP

// #include "utils.hpp"

/// Immutable point expressed in World coordinates
public class WorldPoint
{
    // Public methods
    public WorldPoint(UInt32 x, UInt32 y, WorldDimension dim)
    {
        _x = x;
        _y = y;
        Assert.IsTrue(_x < dim.getWidth() && _y < dim.getHeight(), "Point outside of world!");
    }

    public WorldPoint(WorldPoint other)
    {
        _x = other.x();
        _y = other.y();
    }

    public UInt32 x()
    {
        return _x;
    }
    public UInt32 y()
    {
        return _y;
    }
    public UInt32 ToIndex(WorldDimension dim)
    {
        Assert.IsTrue(_x < dim.getWidth() && _y < dim.getHeight(), "Point outside of world!");
        return _y * dim.getWidth() + _x;
    }
    
    // Private methods
    private UInt32 _x {get;}
    private UInt32 _y {get;}
}
