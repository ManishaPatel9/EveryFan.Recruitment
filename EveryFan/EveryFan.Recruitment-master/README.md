# EveryFan.Recruitment

Welcome to Everyfan's C# technical test. To get started, fork this repository. Once you've finished making changes send us a pull request, **please include your name in the pull request**. Don't spend more than an hour or two on the test.

## Introduction

Everyfan runs sports betting tournaments. These tournaments can pay out to winners in one of two ways:

* Winner takes all - the winner gets the entire prizepool
* 50/50 - players in the top half of the table win twice their stake, if there are an odd number of players the player in the middle position wins their stake back.

In either case in the event of players tying for places they should be paid out the sum of the payouts due to their finishing positions divided equally among them.

## Tasks

1.  Refactor the FiftyFiftyPayoutCalculator and WinnerTakesAllPayoutCalculator classes to remove the duplicated code.

2.  Provide an implementation for the GetPayingPositions method in both classes so the existing TwoEntries and ThreeEntries tests pass for both.

3.  Make changes so that the SplitWinnings tests pass for both classes. Remember, tied positions should be paid out an equal split of the total payouts due to their finishing positions.

4.  Modify the PayoutEngine class so payout calculators are created by a factory class passed as a constructor parameter.

5.  Modify the FiftyFiftyPayoutCalculator class so that the OddSplitWinnings test passes. In the event of payouts not being equally divisible among the winners the remainder should be randomly distributed.

6.  Write some more tests to demonstrate that your solution works in other cases.






