package com.example.demo.services;

import com.example.demo.models.Widget;
import com.example.demo.repositories.WidgetRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class WidgetService {
    List<Widget> widgetList = new ArrayList<Widget>();

    @Autowired
    WidgetRepository widgetRepository;

    public Widget createWidget(Widget newWidget) {
        return widgetRepository.save(newWidget);
    }

    public Widget findWidgetById(Integer wid) {
        return widgetRepository.findWidgetById(wid);
//        return widgetRepository.findById(wid).get();
    }

    public List<Widget> findAllWidgets() {
        return widgetRepository.findAllWidgets();
//        return (List<Widget>)widgetRepository.findAll();
    }

    public List<Widget> findWidgetsForTopic(String topicId) {
        return widgetRepository.findWidgetsForTopic(topicId);
    }

    public int deleteWidget(Integer wid) {
        widgetRepository.deleteById(wid);
        return 1;
    }

    public int updateWidget(String wid, Widget updatedWidget) {
        for (int i = 0; i < widgetList.size(); i++) {
            if (widgetList.get(i).getId().equals(wid)) {
                widgetList.set(i, updatedWidget);
                return 1;
            }
        }
        return 0;
    }
}
