from django.db import models


class Question(models.Model): 
      Time = models.DateTimeField()
      Open = models.DecimalField(max_digits=10,decimal_places=3)
      High = models.DecimalField(max_digits=10,decimal_places=3)
      Low = models.DecimalField(max_digits=10,decimal_places=3)
      Choise = models.DecimalField(max_digits=10,decimal_places=3)
      Volume = models.IntegerField()

      def __str__(self):
          return self.Time