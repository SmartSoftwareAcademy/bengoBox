from django.contrib import admin
from django.contrib.admin import AdminSite
from .models import *
# Register your models here.


class MyAdminSite(AdminSite):
    def each_context(self, request):
        already_dict = super().each_context(request)
        for name in already_dict['available_apps']:
            # here you can access current user as request.user
            # you can change model name with name['models'][0]['name'] = 'some name you want'
            # name['models'][0]['name'] = 'Users'
            print(name['models'][0]['name'])
        return already_dict


# admin.site = MyAdminSite()
admin.site.register(EmailConfig)
admin.site.register(MyUser)
