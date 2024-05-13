A = 1; 
f = 1; 
phi = 0; 

t = linspace(0, 2*pi, 100);

offset1 = 0.2; 
offset2 = 0.3; 
offset3 = 0.3; 
offset4 = 0.2; 


y1_smooth = smooth(rand(size(t)), 0.1, 'loess'); 
y1 = offset1 * (0.5 + y1_smooth - 0.5);
y2_smooth = smooth(rand(size(t)), 0.2, 'loess'); 
y2 = offset2 * (0.5 + y2_smooth - 0.5); 
y3_smooth = smooth(rand(size(t)), 0.4, 'loess'); 
y3 = offset3 * (0.5 + y3_smooth - 0.5); 
y4_smooth = smooth(rand(size(t)), 0.6, 'loess'); 
y4 = offset4 * (0.5 + y4_smooth - 0.5); 

y_result = y1 + y2 + y3 + y4;

figure;
subplot(2,2,1);
h1 = plot(t(1), y1(1)*100, 'r'); 
xlabel('Время');
ylabel('Цена товара');
title(['Агент_1 (1.0, 0.0)']);

subplot(2,2,2);
h2 = plot(t(1), y2(1)*100, 'g'); 
xlabel('Время');
ylabel('Цена товара');
title(['Агент_2 (0.75, 0.25)']);

subplot(2,2,3);
h3 = plot(t(1), y3(1)*100, 'b'); 
xlabel('Время');
ylabel('Цена товара');
title(['Агент_3 (0.5, 0.5)']);

subplot(2,2,4);
h4 = plot(t(1), y4(1)*100, 'm'); 
xlabel('Время');
ylabel('Цена товара');
title(['Агент_2 (0.75, 0.75)']);

figure;
h_result = plot(t(1), y_result(1)*100/4, 'k'); 
xlabel('Время');
ylabel('Цена товара');
title('Результирующая цена товара');

for i = 2:length(t) 
    set(h1, 'XData', t(1:i), 'YData', y1(1:i)*100);
    set(h2, 'XData', t(1:i), 'YData', y2(1:i)*100);
    set(h3, 'XData', t(1:i), 'YData', y3(1:i)*100);
    set(h4, 'XData', t(1:i), 'YData', y4(1:i)*100);
    set(h_result, 'XData', t(1:i), 'YData', y_result(1:i)*100/4);
    drawnow;
    pause(0.01);
end
