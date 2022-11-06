﻿using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Wavepool
{
    public class RippleSet
    {
        RippleParameters[] rippleParams;
        RippleParameters centreRippleParams;
        Wavepool wavepool;
        Vector2 centre;

        public RippleSet(Wavepool wavepool, RippleParameters[] rippleParams, RippleParameters centreRippleParams, Vector2 screenSize)
        {
            this.wavepool = wavepool;
            this.rippleParams = rippleParams;
            this.centreRippleParams = centreRippleParams;

            centre = screenSize / 2;
        }

        public void SpawnCentreRipple(float centreRadius)
        {
            centreRippleParams.radius = centreRadius;
            wavepool.AddRipple(new Ripple(centre, centreRippleParams));
        }

        public void SpawnRipple(Vector2 origin, int radialIndex)
        {
            RippleParameters p = rippleParams[0];
            if (radialIndex >= 0 && radialIndex < rippleParams.Length)
                p = rippleParams[radialIndex];
            else
                Debug.WriteLine("Attempted to get radial paramters from outside of the array. Index: " + radialIndex);

            wavepool.AddRipple(new Ripple(origin, p));
        }
    }

    public struct RippleParameters
    {
        public float radius;
        public float amplitude;
        public float speed;
        public float period;
        public float crestCount;
        public float decayRate;

        public RippleParameters(float radius, float amplitude, float speed, float period, float crestCount, float decayRate)
        {
            this.radius = radius;
            this.amplitude = amplitude;
            this.speed = speed;
            this.period = period;
            this.crestCount = crestCount;
            this.decayRate = decayRate;
        }

        public RippleParameters Lerp(RippleParameters target, float f)
        {
            RippleParameters result = new RippleParameters();
            result.radius = MathHelper.Lerp(radius, target.radius, f);
            result.amplitude = MathHelper.Lerp(amplitude, target.amplitude, f);
            result.speed = MathHelper.Lerp(speed, target.speed, f);
            result.period = MathHelper.Lerp(period, target.period, f);
            result.crestCount = MathHelper.Lerp(crestCount, target.crestCount, f);
            result.decayRate = MathHelper.Lerp(decayRate, target.decayRate, f);
            return result;
        }
    }
}