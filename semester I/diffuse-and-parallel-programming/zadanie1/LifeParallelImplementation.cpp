/*
 * LifeParallelImplementation.cpp
 */

#include "LifeParallelImplementation.h"

int processRank;
int numberOfProcess;

int processTableRowsAmount;
int processTableFirstRow;

MPI_Request request;

bool firstIteration;

LifeParallelImplementation::LifeParallelImplementation()
= default;

void LifeParallelImplementation::realStep() {
    int currentState, currentPollution;
    if (!firstIteration) {
        if (processRank > 0) {

            MPI_Recv(cells[processTableFirstRow - 1], size_1, MPI_INT, processRank - 1, 0, MPI_COMM_WORLD,
                     MPI_STATUS_IGNORE);

            MPI_Recv(pollution[processTableFirstRow - 1], size_1, MPI_INT, processRank - 1, 1, MPI_COMM_WORLD,
                     MPI_STATUS_IGNORE);
        }
        if (processRank < numberOfProcess - 1) {

            MPI_Recv(cells[processTableFirstRow + processTableRowsAmount], size_1, MPI_INT, processRank + 1, 2,
                     MPI_COMM_WORLD, MPI_STATUS_IGNORE);

            MPI_Recv(pollution[processTableFirstRow + processTableRowsAmount], size_1, MPI_INT, processRank + 1, 3,
                     MPI_COMM_WORLD, MPI_STATUS_IGNORE);
        }
    }
    for (int row = processTableFirstRow; row < processTableFirstRow + processTableRowsAmount; row++)
        for (int col = 1; col < size_1; col++) {
            currentState = cells[row][col];
            currentPollution = pollution[row][col];
            cellsNext[row][col] = rules->cellNextState(currentState, liveNeighbours(row, col),
                                                       currentPollution);
            pollutionNext[row][col] =
                    rules->nextPollution(currentState, currentPollution,
                                         pollution[row + 1][col] + pollution[row - 1][col] + pollution[row][col - 1] +
                                         pollution[row][col + 1],
                                         pollution[row - 1][col - 1] + pollution[row - 1][col + 1] +
                                         pollution[row + 1][col - 1] + pollution[row + 1][col + 1]);


        }
    if (processRank > 0) {

        MPI_Isend(cellsNext[processTableFirstRow], size_1, MPI_INT, processRank - 1, 2, MPI_COMM_WORLD, &request);

        MPI_Isend(pollutionNext[processTableFirstRow], size_1, MPI_INT, processRank - 1, 3, MPI_COMM_WORLD, &request);
    }
    if (processRank < numberOfProcess - 1) {

        MPI_Isend(cellsNext[processTableFirstRow + processTableRowsAmount - 1], size_1, MPI_INT, processRank + 1, 0,
                  MPI_COMM_WORLD, &request);

        MPI_Isend(pollutionNext[processTableFirstRow + processTableRowsAmount - 1], size_1, MPI_INT, processRank + 1, 1,
                  MPI_COMM_WORLD, &request);
    }
    firstIteration = false;
}

void LifeParallelImplementation::oneStep() {
    realStep();
    MPI_Barrier(MPI_COMM_WORLD);
    swapTables();
}

int LifeParallelImplementation::numberOfLivingCells() {
    return sumTable(cells);
}

double LifeParallelImplementation::averagePollution() {
    return (double) sumTable(pollution) / size_1_squared / rules->getMaxPollution();
}

void LifeParallelImplementation::beforeFirstStep() {

    firstIteration = true;

    MPI_Comm_rank(MPI_COMM_WORLD, &processRank);
    MPI_Comm_size(MPI_COMM_WORLD, &numberOfProcess);

    processTableRowsAmount = size / numberOfProcess;
    processTableFirstRow = processRank * processTableRowsAmount;

    if (!processRank) {
        processTableFirstRow++;
        processTableRowsAmount--;
    }

    if (processRank + 1 == numberOfProcess) {
        processTableRowsAmount = processTableRowsAmount - 1 + (size % numberOfProcess);
    }

    if (processRank) {
        int lastRowToReceive = processTableFirstRow + processTableRowsAmount;

        for (int i = processTableFirstRow - 1; i <= lastRowToReceive; i++) {
            MPI_Recv(cells[i], size_1, MPI_INT, 0, i * 100,
                     MPI_COMM_WORLD, MPI_STATUS_IGNORE);
            MPI_Recv(pollution[i], size_1, MPI_INT, 0, i * 100 + 1,
                     MPI_COMM_WORLD, MPI_STATUS_IGNORE);
        }
    }
    if (!processRank) {
        int rowsToSend;
        int firstIndex;
        for (int process = 1; process < numberOfProcess; process++) {
            rowsToSend = size / numberOfProcess;
            firstIndex = process * rowsToSend;
            int lastRow;
            if (process + 1 == numberOfProcess) {
                lastRow = size_1;
            } else {
                lastRow = firstIndex + rowsToSend;
            }
            for (int i = firstIndex - 1; i <= lastRow; i++) {
                MPI_Send(cells[i], size_1, MPI_INT, process, i * 100, MPI_COMM_WORLD);
                MPI_Send(pollution[i], size_1, MPI_INT, process, i * 100 + 1, MPI_COMM_WORLD);
            }
        }
    }
}

void LifeParallelImplementation::afterLastStep() {


    if (!firstIteration) {
        if (processRank > 0) {

            MPI_Recv(cells[processTableFirstRow - 1], size_1, MPI_INT, processRank - 1, 0, MPI_COMM_WORLD,
                     MPI_STATUS_IGNORE);

            MPI_Recv(pollution[processTableFirstRow - 1], size_1, MPI_INT, processRank - 1, 1, MPI_COMM_WORLD,
                     MPI_STATUS_IGNORE);
        }
        if (processRank < numberOfProcess - 1) {

            MPI_Recv(cells[processTableFirstRow + processTableRowsAmount], size_1, MPI_INT, processRank + 1, 2,
                     MPI_COMM_WORLD, MPI_STATUS_IGNORE);

            MPI_Recv(pollution[processTableFirstRow + processTableRowsAmount], size_1, MPI_INT, processRank + 1, 3,
                     MPI_COMM_WORLD, MPI_STATUS_IGNORE);
        }
    }


    if (processRank) {

        for (int i = processTableFirstRow; i < processTableFirstRow + processTableRowsAmount; i++) {
            MPI_Isend(cells[i], size_1, MPI_INT, 0, i * 10,
                      MPI_COMM_WORLD, &request);
            MPI_Isend(pollution[i], size_1, MPI_INT, 0, i * 10 + 1,
                      MPI_COMM_WORLD, &request);
        }
    }
    if (!processRank) {

        int rowsToCollect;
        int firstIndex;
        for (int process = 1; process < numberOfProcess; process++) {

            rowsToCollect = size / numberOfProcess;
            firstIndex = process * rowsToCollect;

            if (process == numberOfProcess - 1) {
                rowsToCollect = rowsToCollect - 1 + (size % numberOfProcess);
            }

            for (int i = firstIndex; i < firstIndex + rowsToCollect; i++) {
                MPI_Recv(cells[i], size_1, MPI_INT, process, i * 10, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
                MPI_Recv(pollution[i], size_1, MPI_INT, process, i * 10 + 1, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
            }
        }
    }

}
