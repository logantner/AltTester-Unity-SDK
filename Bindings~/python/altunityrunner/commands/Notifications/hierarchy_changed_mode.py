from enum import Enum, unique


@unique
class HierarchyMode(Enum):
    Add = 0
    Destroy = 1
    Update = 2

    @classmethod
    def values(cls):
        return [hierarchy_mode.value for hierarchy_mode in cls]
