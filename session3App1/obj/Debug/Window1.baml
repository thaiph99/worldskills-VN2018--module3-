   M S B A M L   `   `   `  ����G  Csession3App1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:session3App1session3App1  5   	       session3App1.Window1   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:session3App1     5   	     � Title$  Booking confirmation��5   	   $	��627��6&   $	��806��63    � DataContext$ public��6?   $��1��6T   .��5	      � $��
1,0,13,-53q�6    � RowDefinitions 5
   
   �� 5      $
��133* �6   �� 5      $
��516* �6   .+�5   
   �� $�� ��6   $��Outbound flight details��6   $
��Left=�6?   $��95��6Z   $��	25,10,0,0q�6f   $	��Top=�6y   $	��751��6�   �� 5   
   -   5        $��v6��5      $��Return flight details��6   $
��Left=�6<   $��95��6W   $��
25,131,0,0q�6c   $	��Top=�6w   $	��751��6�   $��2��6�   �� 5   
   $��Passenger details��6   $
��Left=�6.   $	��143��6I   $��
25,106,0,0q�6V   $	��Top=�6j   $	��751��6�   $��1��6�   �� 5   
   -   5        $��	btnRemove��5      $��Remove passenger��6"   $
��Left=�6=   $��24��6X   $��634,401,0,0q�6d   $	��Top=�6y   $	��122��6�   $��1��6�   �� 5   
   $��	Firstname��6   $
��Left=�6$   $��25��6?   $��
43,137,0,0q�6K   $	��Top=�6_   $��62��6w   $��1��6�   �� 5   
   -   5        $��	txtFiName��5      $��1��6   $
��Left=�60   $��25��6K   $��120,137,0,0q�6W    �� TextWrapping$
 Wrap=�6l   $�� ��6�   $	��Top=�6�   $��92��6�   $��1��6�   �� 5   
   $��Passport number��6   $
��Left=�6*   $��25��6E   $��
30,178,0,0q�6Q   $	��Top=�6e   $	��104��6}   $��1��6�   �� 5   
   -   5        $��
txtPassNum��5      $��4��6   $
��Left=�61   $��25��6L   $��139,178,0,0q�6X   $
 Wrap=�6m   $�� ��6�   $	��Top=�6�   $��92��6�   $��1��6�   �� 5   
   $��Lastname��6   $
��Left=�6#   $��25��6>   $��256,137,0,0q�6J   $	��Top=�6_   $��65��6w   $��1��6�   �� 5   
   -   5        $��	txtLaName��5      $��2��6   $
��Left=�60   $��25��6K   $��328,137,0,0q�6W   $
 Wrap=�6l   $�� ��6�   $	��Top=�6�   $��92��6�   $��1��6�   �� 5   
   $��Passport country��6   $
��Left=�6+   $��25��6F   $��253,178,0,0q�6R   $	��Top=�6g   $��98��6   $��1��6�   �� 5   
   $��	Birthdate��6   $
��Left=�6$   $��25��6?   $��478,137,0,0q�6K   $	��Top=�6`   $��71��6x    J� RenderTransformOrigin$ 	0.5,0.493&�6�   $��1��6�   �� 5   
   $��Phone��6   $
��Left=�6    $��25��6;   $��505,178,0,0q�6G   $	��Top=�6\   $��44��6t   $��1��6   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��dpBirth��5   "   $��3��6   $
��Left=�61   $��25��6L   $��585,137,0,0q�6X   $	��Top=�6m   $	��127��6�   $��1��6�   �� 5   
   -   5        $��txtPhone��5      $��6��6   $
��Left=�6/   $��25��6J   $��585,178,0,0q�6V   $
 Wrap=�6k   $�� ��6   $	��Top=�6�   $	��127��6�   $��1��6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��dgvPassList��5         
IsReadOnly .    AutoGenerateColumns .  $
��Left=�6T   $	��106��6o   $��
25,279,0,0q�6|   $	��Top=�6�   $	��731��6�   $��1��6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Firstname��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 100 6R   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 69   �� *Col1+6R     5       $ Lastname��6%   $		 100 6Q   
 68   �� *Col2+6Q     5!      $ 	Birthdate��6%   $		 120 6R   
 69   �� *Col3+6R     5"      $ Passport number��6%   $		 130 6X   
 6?   �� *Col4+6X     5#      $ Passport country��6%   $		 130 6Y   
 6@   �� *Col5+6Y     5$      $ Phone��6%   $		 130 6N   
 65   �� *Col6+6N   5&      �� 5(   
   $��Passengers list��6   $
��Left=�6*   $��25��6E   $��
31,256,0,0q�6Q   $	��Top=�6e   $	��142��6}   $��1��6�   �� 5)   
   -	   5        $��	cbbPassCT��5)       $��5��6   $
��Left=�61   $��25��6L   $��369,178,0,0q�6X   $	��Top=�6m   $��92��6�   $��1��6�   �� 5*   
   -
   5        $��btnBack��5*      $ ��Back to search for flights��6    $
��Left=�6E   $��24��6`   $��158,408,0,0q�6l   $	��Top=�6�   $	��193��6�   $��1��6�   �� 5+   
   -   5        $��
btnConfirm��5+      $��Confirm booking��6#   $
��Left=�6=   $��24��6X   $��397,408,0,0q�6d   $	��Top=�6y   $	��152��6�   $��1��6�   �� 5,   
   -   5        $��btnAdd��5,      $��Add passenger��6   $
��Left=�67   $��24��6R   $��590,216,0,0q�6^   $	��Top=�6s   $	��122��6�   $��1��6�   �� 5-   
   $��From : ��6   $
��Left=�6"   $��28��6=   $��	43,38,0,0q�6I   $	��Top=�6\   $��47��6t   �� 5.   
   -   5        $��lbFrom��5.      $��Test1��6   $
��Left=�6.   $��28��6I   $��	79,38,0,0q�6U   $	��Top=�6h   $��51��6�   �� 5/   
   $��To : ��6   $
��Left=�6    $��28��6;   $��
135,38,0,0q�6G   $	��Top=�6[   $��47��6s   �� 50   
   -   5         ��Name$
 lbTo��50      $��Test2��6   $
��Left=�6.   $��28��6I   $��
157,38,0,0q�6U   $	��Top=�6i   $��60��6�   �� 51   
   $��Cabin type : ��6   $
��Left=�6(   $��28��6C   $��
222,38,0,0q�6O   $	��Top=�6c   $��73��6{   �� 52   
   -   5        $ lbCabin��52      $��First Class��6!   $
��Left=�67   $��28��6R   $��
289,38,0,0q�6^   $	��Top=�6r   $��79��6�   �� 53   
   $��Date : ��6   $
��Left=�6"   $��28��6=   $��
380,38,0,0q�6I   $	��Top=�6]   $��49��6u   �� 54   
   -   5        $ lbDate��54      $��
77/07/7777��6    $
��Left=�65   $��28��6P   $��
429,38,0,0q�6\   $	��Top=�6p   $��86��6�   �� 55   
   $��Flight Number(s) : ��6   $
��Left=�6.   $��28��6I   $��
540,38,0,0q�6U   $	��Top=�6i   $	��111��6�   �� 56   
   -   5        $��lbFlightNum��56      $��	6 - 6 - 6��6#   $
��Left=�67   $��28��6R   $��
651,38,0,0q�6^   $	��Top=�6r   $	��105��6�   �� 57   
   -   5        $��v1��57      $��From : ��6   $
��Left=�6,   $��28��6G   $��	44,35,0,0q�6S   $	��Top=�6f   $��47��6~   $��1��6�   �� 58   
   -   5        $ lbFrom2��58      $��Test1��6!   $
��Left=�61   $��28��6L   $��	80,35,0,0q�6X   $	��Top=�6k   $��51��6�   $��1��6�   �� 59   
   -   5        $��v2��59      $��To : ��6   $
��Left=�6*   $��28��6E   $��
136,35,0,0q�6Q   $	��Top=�6e   $��47��6}   $��1��6�   �� 5:   
   -   5        $ lbTo2��5:      $��Test2��6   $
��Left=�6/   $��28��6J   $��
158,35,0,0q�6V   $	��Top=�6j   $��60��6�   $��1��6�   �� 5;   
   -   5        $��v3��5;      $��Cabin type : ��6   $
��Left=�62   $��28��6M   $��
223,35,0,0q�6Y   $	��Top=�6m   $��73��6�   $��1��6�   �� 5<   
   -   5        $ lbCabin2��5<      $��First Class��6"   $
��Left=�68   $��28��6S   $��
290,35,0,0q�6_   $	��Top=�6s   $��79��6�   $��1��6�   �� 5=   
   -   5        $��v4��5=      $��Date : ��6   $
��Left=�6,   $��28��6G   $��
381,35,0,0q�6S   $	��Top=�6g   $��49��6   $��1��6�   �� 5>   
   -   5        $ lbDate2��5>      $��
77/07/7777��6!   $
��Left=�66   $��28��6Q   $��
430,35,0,0q�6]   $	��Top=�6q   $��86��6�   $��1��6�   �� 5?   
   -   5        $��v5��5?      $��Flight Number(s) : ��6   $
��Left=�68   $��28��6S   $��
541,35,0,0q�6_   $	��Top=�6s   $	��111��6�   $��1��6�   �� 5@   
   -   5        $ lbFlightNum2��5@      $��	6 - 6 - 6��6&   $
��Left=�6:   $��28��6U   $��
652,35,0,0q�6a   $	��Top=�6u   $	��104��6�   $��1��6�   �� 5A   
   $.��(E:\Window form\GPPM_CNTT\assets\back.png��6   $
��Left=�6B   $��16��6]   $��164,412,0,0q�6i   $	��Top=�6~   $��19��6�   $��1��6�   �� 5B   
   $.��(E:\Window form\GPPM_CNTT\assets\tick.png��6   $
��Left=�6B   $��16��6]   $��405,412,0,0q�6i   $	��Top=�6~   $��19��6�   $��1��6�   5D      5E      