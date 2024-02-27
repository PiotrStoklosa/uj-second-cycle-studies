/*
 * LifeParallelImplementation.h
 */

#ifndef LIFEPARALLELIMPLEMENTATION_H
#define LIFEPARALLELIMPLEMENTATION_H


#include "Life.h"
#include <mpi.h>


class LifeParallelImplementation : public Life {
protected:
    void realStep() override;

public:
    LifeParallelImplementation();

    int numberOfLivingCells() override;

    double averagePollution() override;

    void oneStep() override;

    void beforeFirstStep() override;

    void afterLastStep() override;
};


#endif //LIFEPARALLELIMPLEMENTATION_H
