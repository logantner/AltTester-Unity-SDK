from enum import IntFlag, auto


class NotificationType(IntFlag):
    NONE = 0
    LOADSCENE = auto()
    UNLOADSCENE = auto()
    ALL = LOADSCENE | UNLOADSCENE
