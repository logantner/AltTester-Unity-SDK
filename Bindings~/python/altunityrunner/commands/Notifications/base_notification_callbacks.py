from loguru import logger


class BaseNotificationCallbacks():
    def scene_loaded_callback(self, load_scene_notification_result):
        logger.debug("Scene {0} was loaded {1}".format(str(load_scene_notification_result.scene_name),
                                                       str(load_scene_notification_result.loadSceneMode)))

    def hierarchy_changed_callback(self, hierarchy_changed_notification_result):
        logger.debug("Object {0} was {1}".format(str(hierarchy_changed_notification_result.object_name),
                                                str(hierarchy_changed_notification_result.hierarchy_mode)))
    def scene_unloaded_callback(self, scene_name):
        logger.debug("Scene {0} was unloaded".format(scene_name))
