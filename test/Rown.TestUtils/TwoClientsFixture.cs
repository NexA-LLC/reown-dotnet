﻿namespace Reown.TestUtils;

public abstract class TwoClientsFixture<TClient> where TClient : IDisposable
{
    public TwoClientsFixture(bool initNow = true)
    {
        if (initNow)
            Init();
    }

    public TClient ClientA { get; protected set; }
    public TClient ClientB { get; protected set; }

    public abstract Task Init();

    public async Task WaitForClientsReady()
    {
        while (Equals(ClientA, default(TClient)) && Equals(ClientB, default(TClient)))
            await Task.Delay(10);
    }

    public virtual async Task DisposeAndReset()
    {
        if (!Equals(ClientA, default(TClient)))
        {
            ClientA.Dispose();
            ClientA = default;
        }

        if (!Equals(ClientB, default(TClient)))
        {
            ClientB.Dispose();
            ClientB = default;
        }

        await Task.Delay(500);

        await Init();
    }
}