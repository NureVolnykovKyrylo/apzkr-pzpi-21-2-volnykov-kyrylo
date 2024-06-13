#ifndef FEEDING_SCHEDULE_H
#define FEEDING_SCHEDULE_H

struct FeedingSchedule {
  int feedingScheduleId;
  int aquariumId;
  float feedTime;
  float feedAmount;
  String feedType;
  float repeatInterval;
  bool active;
};

#endif // FEEDING_SCHEDULE_H