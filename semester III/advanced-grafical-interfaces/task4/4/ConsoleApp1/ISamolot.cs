using System;
public interface ISamolot
{
    void PerformAction();
    void AdjustSeating();
    bool IsAvailable { get; set; }
}
