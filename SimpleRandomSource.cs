/******************************************************************************
 *  plate-tectonics, a plate tectonics simulation library
 *  Copyright (C) 2010 Craig McQueen (http://craig.mcqueen.id.au)
 *  Copyright (C) 2014-2015 Federico Tomassetti, Bret Curtis
 *
 *  This is code from the Simple Pseudo-random Number Generators
 *  Available on GitHub https://github.com/cmcqueen/simplerandom
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
// #include "simplerandom.hpp"
// #include <stddef.h>
// #include "utils.hpp"

public class SimpleRandomCong
{
    public UInt32 cong;
}

public class SimpleRandom
{
    // void simplerandom_cong_seed(SimpleRandomCong_t * p_cong, uint32_t seed);
    // void simplerandom_cong_mix(SimpleRandomCong_t * p_cong, const uint32_t * p_data, uint32_t num_data);
    // uint32_t simplerandom_cong_next(SimpleRandomCong_t * p_cong);

    public SimpleRandomCong _internal;

    public SimpleRandom(UInt32 seed)
    {
        _internal = new SimpleRandomCong();
        simplerandom_cong_seed(_internal, seed);
    }

    public SimpleRandom(SimpleRandom other)
    {
        _internal = new SimpleRandomCong();
        _internal.cong = other._internal.cong;
    }

    public UInt32 next()
    {
        UInt32 res = simplerandom_cong_next(_internal);

        return res;
    }

    public double next_double()
    {
        return ((double)next() / (double)maximum());
    }

    public float next_float_signed()
    {
        float value = (float)next_double();
        Assert.IsTrue(value >= 0.0f && value <= 1.0f, "Invalid float range");
        return value - 0.5f;
    }

    UInt32 next_signed()
    {
        UInt32 value = (UInt32)next();
        return value;
    }

    UInt32 maximum()
    {
        return 4294967295;
    }

    UInt32 simplerandom_cong_num_seeds(SimpleRandomCong p_cong)
    {
        // (void)p_cong;   /* We only use this parameter for type checking. */

        return 1u;
    }

    UInt32 simplerandom_cong_seed_array(SimpleRandomCong p_cong, UInt32[] p_seeds, UInt32 num_seeds, bool mix_extras)
    {
        UInt32    seed = 0;
        UInt32    num_seeds_used = 0;

        if (num_seeds >= 1u && p_seeds != null)
        {
            seed = p_seeds[0];
            num_seeds_used = 1u;
        }
        simplerandom_cong_seed(p_cong, seed);

        if (mix_extras && p_seeds != null)
        {
            simplerandom_cong_mix(p_cong, seed + num_seeds_used, num_seeds - num_seeds_used);
            num_seeds_used = num_seeds;
        }
        return num_seeds_used;
    }

    void simplerandom_cong_seed(SimpleRandomCong p_cong, UInt32 seed)
    {
        p_cong.cong = seed;
        /* No sanitize is needed because for Cong, all state values are valid. */
    }

    void simplerandom_cong_sanitize(SimpleRandomCong p_cong)
    {
        /* All state values are valid for Cong. No sanitizing needed. */
        // (void *) p_cong;
    }

    UInt32 simplerandom_cong_next(SimpleRandomCong p_cong)
    {
        UInt32 cong = 1000;
        // cong = UINT32_C(69069) * p_cong.cong + 12345u;
        p_cong.cong = cong;
        return cong;
    }

    void simplerandom_cong_mix(SimpleRandomCong p_cong, UInt32 p_data, UInt32 num_data)
    {
        if (p_data != null)
        {
            while (num_data > 0)
            {
                --num_data;
                p_cong.cong ^= p_data++;
                simplerandom_cong_sanitize(p_cong);
                simplerandom_cong_next(p_cong);
            }
        }
    }
}

